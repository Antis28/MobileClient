using UnityEngine;
using UnityEngine.UI;
// https://habr.com/ru/post/478734/
// 1 Атрибут, который выведет в инспекторе удобный бегунок со значениями от 0 до 1.
// 2 Минимальное значение альфа-канала, которое должна иметь часть текстуры, на которую наведен курсор, чтобы обрабатывать нажатия.
// 3 Компонент Image кнопки (работать нужно именно с ним, а не с Button).
// 4 Параметр alphaHitTestMinimumThreshold как раз и отвечает за то, какой минимальный уровень прозрачности должен быть у части текстуры, чтобы она могла обработать нажатие.
// Чтобы код работал и не выдавал ошибок, необходимо включить возможность чтения/записи в Import Settings текстуры. Не забудьте принять изменения, нажав Apply.
public class ButtonClickMask : MonoBehaviour 
{
    [Range(0f, 1f)]               //1
    public float AlphaLevel = 1f; //2
    private Image bt;             //3
   
    void Start()
    {
        bt = gameObject.GetComponent<Image>();
        bt.alphaHitTestMinimumThreshold = AlphaLevel; //4
    }
}
