using Engine;

public class NumberWizard : EngineMain
{
    private int max;
    private int min;
    private int guess;

    public override void Initialize()
    {

    }

    public override void LoadContent()
    {
        NewGame();
    }

    private void NewGame()
    {
        max = 1000;
        min = 1;

        Console.WriteLine();
        Console.WriteLine("=============================================");
        Console.WriteLine();
        Console.WriteLine("Welcome to Number Wizard.");
        Console.WriteLine();
        Console.WriteLine("Pick a number in your head, but don't tell me!");

        Console.WriteLine();
        Console.WriteLine("The highest number you can pick is " + max);
        Console.WriteLine();
        Console.WriteLine("The lowest number you can pick is " + min);

        Guess();

        max = max + 1;
    }

    public override void Update()
    {
        if(Input.GetKeyDown(Actions.Up))
        {
            min = guess;
            Guess();
        }

        if (Input.GetKeyDown(Actions.Down))
        {
            max = guess;
            Guess();
        }

        if (Input.GetKeyDown(Actions.Action))
        {
            Console.WriteLine();
            Console.WriteLine("Correct!");

            NewGame();
        }
    }

    private void Guess()
    {
        guess = (max + min) / 2;

        Console.WriteLine();
        Console.WriteLine("Is the number higher or lower than " + guess + "?");
        Console.WriteLine();
        Console.WriteLine("Press Up for higher, Down for lower, or Return for correct guess.");
    }

    public override void Draw()
    {

    }
}