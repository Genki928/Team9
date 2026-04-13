using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] character_list;
    /// <summary> 配列0番目：Player1<br></br>配列1番目：Player2 </summary>
    int[] player_pick = new int[2] { 0, 0 };
    /// <summary> 配列0番目：Player1<br></br>配列1番目：Player2 </summary>
    GameObject[] character = new GameObject[2];
    Vector3[] spawn_pos = new Vector3[2];

    void Awake()
    {
        CharacterBase.OnDie += FinishBattle;
    }

    void Start()
    {
        spawn_pos[0] = new Vector3(3, 0, 3);
        spawn_pos[1] = new Vector3(-3, 0, -3);
        for (int i = 0; i < 2; ++i)
        {
            character[i] = Instantiate(character_list[player_pick[i]], spawn_pos[i], Quaternion.identity);
            if (character[i].TryGetComponent<CharacterBase>(out var cb)) cb.SetID(i);
        }
    }

    void Update()
    {
        
    }

    /// <summary> 配列0番目：Player1<br></br>配列1番目：Player2 </summary>
    /// <param name="id"> どっちのプレイヤーがやられたか、識別用のid </param>
    void FinishBattle(int id)
    {
        if (id == 0)
        {
            Debug.Log("プレイヤー1の勝利");
        }
        else
        {
            Debug.Log("プレイヤー2の勝利");
        }
    }
}
