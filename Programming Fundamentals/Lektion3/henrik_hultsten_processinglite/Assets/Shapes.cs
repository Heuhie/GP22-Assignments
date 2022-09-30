
public class Shapes : ProcessingLite.GP21
{

    public float xSquare = 5f;
    public float ySquare = 5f;
    public float diameter = 2f;
    public float xCircle = 10f;
    public float yCircle = 5f;
    public float size = 2f;


    void Update()
    {
        Background(200); //Clears the background and sets the color to 0.
        Stroke(200);
        Fill(255, 0, 0);
        Square(xSquare, ySquare, size);

        Stroke(200);
        Fill(0, 255, 0);
        Rect(xCircle, yCircle, xCircle +3, yCircle +1);
    }
    
}
