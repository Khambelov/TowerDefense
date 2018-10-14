using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    [SerializeField, Tooltip("Проверка на направление движения вверх. Используется для поворота противника. Если значение верно, противник поворачивает налево. Иначе направо.")]
    private bool rotateRight;

    public bool CheckPoint { get { return rotateRight; } }
}
