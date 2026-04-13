using System;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    Rigidbody rb;
    public static event Action<int> OnDie;

    [Header("< 基礎ステータス >")]
    [SerializeField] CharacterData data;
    public int id { get; private set; }
    public int burst_gage { get; protected set; } = 0;
    public int max_burst_gage { get; protected set; }
    public float speed { get; protected set; }
    bool can_move = false;


    void Start()
    {
        // 物理の取得
        rb = GetComponent<Rigidbody>();

        // ScriptableObjectからの読み込み
        max_burst_gage = data.max_burst_gage;
        speed = data.speed;
    }

    void Update()
    {
        // デバッグ用自爆
        if(Input.GetKeyDown(KeyCode.Backspace) && id == 0)
        {
            ChangeBurstGage(10);
        }
    }

    void FixedUpdate()
    {
        // 移動可能かどうかチェック
        if (can_move)
        {
            // キーボード操作（デバッグ用）
            if (Input.GetKey(KeyCode.W)) rb.linearVelocity = transform.forward * data.speed;
            if (Input.GetKey(KeyCode.S)) rb.linearVelocity = transform.forward * -data.speed;
            if (Input.GetKey(KeyCode.D)) rb.linearVelocity = transform.right * data.speed;
            if (Input.GetKey(KeyCode.A)) rb.linearVelocity = transform.right * -data.speed;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        // 接地しているなら、移動を許可
        if (col.gameObject.CompareTag("Ground"))
        {
            can_move = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        // 接地していないなら、移動を禁止
        if (col.gameObject.CompareTag("Ground"))
        {
            can_move = false;
        }
    }

    /// <summary> キャラクターのidを設定 </summary>
    /// <param name="id"> idの値 </param>
    public void SetID(int id)
    {
        this.id = id;
    }

    /// <summary> バーストゲージを増減させる関数 </summary>
    /// <param name="damage">正の値なら増加、負の値なら減少</param>
    public void ChangeBurstGage(int damage)
    {
        burst_gage += damage;

        // バーストゲージが上限を超えたら、バトルを終了させる
        if (burst_gage >= max_burst_gage)
        {
            OnDie?.Invoke(id);
        }
    }
}
