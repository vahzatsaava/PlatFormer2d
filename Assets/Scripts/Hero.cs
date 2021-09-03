using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private int lives = 5;
    [SerializeField] private float speed = 5f;//скорость движения героя
    [SerializeField] private float jumpForce = 15f;//сила прыжка героя
  
   

    private Rigidbody2D RB;
    private Animator anim;//Поле аниции 
    private SpriteRenderer sprite;
   

    //паттерн синглтон делаем для доступа ко всем публичным полям класса не создавая его экземплярa
    public static Hero Instance { get; set; }
   
    public void GetDamage()//нанесение урона нашему герою
    {
        lives -= 1;
        Debug.Log($"Колличество жизней {lives}");
        //если жизней нет у героя то он исчезает
        if (lives==0)
        {
            gameObject.SetActive(false);

        }
    }
    

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();//получаем ссылку на компонент РИДЖИТ БОДИ
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();//получаем ссылку на компонентт СпратРендерер(он в дочернем элементе)
        Instance = this;//указываем чтобы заработал Синглтон в другом классе
    }
    
    private States State
    {
       get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
   

   private void Update()
    {
        if (isGrounded) State = States.animationStay;//если мы на на земле то анимируется анимация где стоит игрок
               
        Run();
        JumpHero();
        CheckInGround();
    }
   
    private void Run()
    {
          
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
            sprite.flipX = transform.position.x < 0.0f;
            if (isGrounded) State = States.RunAnimation;//выывается анимация когда перонаж двигается направо(на земле)
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);
            sprite.flipX = transform.position.x > 0.0f;
            if (isGrounded) State = States.RunAnimation;//вызывается анимация когда перонаж двигается налево(на земле)
        }
      
    }

    private void JumpHero()
    {
        if ( Input.GetKeyDown(KeyCode.Space)&&isGrounded)//добавляем условие что герой на земле)
        
            RB.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);           
        }


    
  //переменные для прыжка героя
    private bool isGrounded = false;//переменная показывающая на зеемле ли наш объект
    public Transform groundCheck;//перетаскиваем объект граундчек в юнити
    public float checkRadius = 0.5f;//радиус колайдера
    public LayerMask Ground;//хранение слоя который будет считаться землей


   private void CheckInGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, Ground);
        if (!isGrounded) State = States.JumpAnimation;
    }
    
}
/// <summary>
/// Энум для наших Анимаций (Для всех состояний (стоя,бег,прыжок))
/// </summary>
public enum States
{
    animationStay,
    JumpAnimation,
    RunAnimation

}
