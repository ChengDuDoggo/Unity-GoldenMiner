using UnityEngine;
//定义旋转方向枚举
public enum RotaDir
{
    left,
    right,
}
public class Hook : MonoBehaviour
{
    public Transform StartTrans;//钩子起始点
    [HideInInspector]
    public int TotleValue = 0;//当前抓取总价值
    public float AngleSpeed = 50.0f;//钩子旋转速度
    public float MoveSpeed = 50.0f;//钩子发射速度
    public GameObject CurrentGrabItem;//当前抓取物品
    private LineRenderer lineRender;
    private RotaDir nowDir = RotaDir.left;//玩家当前的旋转方向
    private Vector3 hookStartPoint;
    private float hookRecycleSpeed = 50.0f;//钩子回收速度
    private bool isFire;
    private bool isBack;
    private void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        lineRender.startWidth = 1.0f;//修改线条宽度
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!isFire)
            {
                isFire = true;
                hookStartPoint = transform.position;
            }
        }
        //检测触摸输入
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);//获取第一个触摸点
            //如果触摸开始
            if (touch.phase == TouchPhase.Began)
            {
                if (!isFire)
                {
                    isFire = true;
                    hookStartPoint = transform.position;
                }
            }
        }
        if (isFire && !isBack)
        {
            HookMoveForward();//射
        }
        else if (isFire && isBack)
        {
            HookBackMove();//收
        }
        if (!isFire)
        {
            HookRotate();
        }
        UpdateLine();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary"))
        {
            isBack = true;
        }
        if (other.CompareTag("MinerItem") && CurrentGrabItem == null)
        {
            CurrentGrabItem = other.gameObject;
            MinerItem minerItem = other.GetComponent<MinerItem>();
            if (minerItem.ItemSpecie == ItemSpecie.Pig)
            {
                minerItem.IsPig = false;
            }
            hookRecycleSpeed = minerItem.ItemWeight;
            isBack = true;
            CurrentGrabItem.transform.parent = transform;
            CurrentGrabItem.transform.SetPositionAndRotation(transform.TransformPoint(new Vector3(0.0f, -40.0f, 0.0f)), transform.rotation);
        }
    }
    private void UpdateLine()
    {
        //设置线条两点的位置
        lineRender.SetPosition(0, StartTrans.position);
        lineRender.SetPosition(1, transform.position);
    }
    /// <summary>
    /// 钩子左右旋转
    /// </summary>
    private void HookRotate()
    {
        //计算钩子向上向量与向右向量的夹角
        float rightAngle = Vector3.Angle(transform.up, Vector3.right);
        if (nowDir == RotaDir.left)
        {
            if (rightAngle < 170)
            {
                //在可旋转范围内围绕向前向量(朝向屏幕)继续旋转
                transform.RotateAround(StartTrans.position, Vector3.forward, AngleSpeed * Time.deltaTime);
            }
            else
            {
                //超出范围，改变方向进行旋转
                nowDir = RotaDir.right;
            }
        }
        else
        {
            if (rightAngle > 10)
            {
                transform.RotateAround(StartTrans.position, Vector3.forward, -AngleSpeed * Time.deltaTime);
            }
            else
            {
                nowDir = RotaDir.left;
            }
        }
    }
    /// <summary>
    /// 钩子向前移动
    /// </summary>
    private void HookMoveForward()
    {
        transform.position += -1 * MoveSpeed * Time.deltaTime * transform.up;
    }
    /// <summary>
    /// 钩子返回移动
    /// </summary>
    private void HookBackMove()
    {
        transform.position += hookRecycleSpeed * Time.deltaTime * transform.up;
        if (transform.position.y >= hookStartPoint.y)
        {
            transform.position = hookStartPoint;
            isFire = false;
            isBack = false;
            if (CurrentGrabItem)
            {
                TotleValue += CurrentGrabItem.GetComponent<MinerItem>().ItemValue;
                EventManager.instance.CallGoldMinerGetItemEvent();
                Destroy(CurrentGrabItem);
                CurrentGrabItem = null;
                hookRecycleSpeed = 50.0f;
            }
        }
    }
}