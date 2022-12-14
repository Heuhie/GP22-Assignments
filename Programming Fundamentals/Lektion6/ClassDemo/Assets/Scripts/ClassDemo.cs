using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassDemo : ProcessingLite.GP21
{

    [Range(0, 255)]
    public int r, g, b;
    public int numberOfBalls;

    private Ball[] balls;
    private Player player;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        StartNewGame();
    }

    // Update is called once per frame
    void Update()
    {
        //Run while not gameover
        if (!gameOver)
        {
            Background(r, g, b);
            player.GetInput();
            player.UpdateMovement();
            player.DrawPlayer();
            player.Gravity();

            if (player.PlayerCollision(player, balls))
                gameOver = !gameOver;

            //Loops through all balls, update, draw, checkcollision
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i].UpdatePosition();
                balls[i].DrawBall();
                balls[i].CheckBounds();
                balls[i].CheckCollision(balls[i], balls);
            }
        }

        //Gameover screen
        else
        {
            Background(0);
            TextSize(100);
            Fill(0, 255, 0);
            Text("FIN", Width / 2, Height / 2);
            Text("R to Restart", Width / 2, 3);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            gameOver = !gameOver;
            StartNewGame();
        }
    }

    //Initialize new game
    private void StartNewGame()
    {
        Background(r, g, b);
        player = new Player();
        //startPos.x = Random.Range(2, 18);
        //startPos.y = Random.Range(2, 8);

        Vector2 tmpPos = player.GetPosition();
        balls = new Ball[numberOfBalls];

        for (int i = 0; i < balls.Length; i++)
        {
            balls[i] = new Ball();
            CheckSpawn(balls[i], balls, i);
            CheckSpawn(balls[i], player);
        }
    }


    //Not working correctly
    //Check that newly spawned balls doesn't spawn on other balls
    //index indicates where to start in the array
    private void CheckSpawn(Ball ball, Ball[] balls, int index)
    {
        for (int i = index -1; i >= 0; i--)
        {
            Vector2 tmpVector = balls[i].position;
            while (ball.position.x > tmpVector.x - 2 && ball.position.x < tmpVector.x + 2)
            {
                ball.position.x = Random.Range(2, Width - 2);
            }

            while (ball.position.y > tmpVector.y - 2 && ball.position.y < tmpVector.y + 2)
            {
                ball.position.y = Random.Range(2, Width - 2);
            }
        }
    }


    //Check that newly spawned balls doesn't spawn on player
    private void CheckSpawn(Ball ball, Player player)
    {
        Vector2 tmpPVector = player.GetPosition();
        while(ball.position.x > tmpPVector.x - 2 && ball.position.x < tmpPVector.x +2)
        {
            ball.position.x = Random.Range(2, Width - 2);
        }

        while(ball.position.y > tmpPVector.y - 2 && ball.position.y < tmpPVector.y + 2)
        {
            ball.position.y = Random.Range(2, Height -2);
        }
    }

}
