using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructSansyo2 : MonoBehaviour
{
    // # 値型と参照型の違いと使い分け
    // * class/参照型
    // 特性: 生成コストが高い、コピーコストが低い
    // 長寿命（一度作ったら長く使う）、容量（データ量）が大きい、共有する

    // * struct/値型
    // 特性: 生成コストが低い、コピーコストが高い
    // 短命（ローカルスコープ内）、容量が小さい、非共有（使い捨て）

    // Unity 標準型における代表的な struct 型
    // 特徴としては計算用の数学・算術データが中心
    // Vector2, Vector3, Vector4
    // Quaternion
    // Color
    // Ray, RaycastHit
    // Matrix4x4
    // Rect, Bounds
}
