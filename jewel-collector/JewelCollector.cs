using jewel_collector;
public class JewelCollector
{

    delegate void Move(char direction);

    static event Move OnMove; 
    public static void Main() {
        
         // Crie Mapa Dimensao 10x10
        Console.WriteLine("Creating Map");
        int w = 10;
        int h = 10;
        int level = 1;

        while(true){
            try {
                Map mapa = new Map(w, h, level);
                Robot player = new Robot(mapa);
                mapa.setPlayer(player, 0, 0);

                Console.WriteLine($"Level: {level}");
                bool Result = run(player, mapa);

                if(Result) {
                    w++;
                    h++;
                    level++;
                    if(w > 30){
                        Console.WriteLine("WIN");
                        break;
                    }
                } else {
                    break;
                }
            }
            catch(RanOutOfEnergyException e) {
                Console.WriteLine("Robot ran out of energy!");
            }
        }
    }

    private static bool run(Robot robot, Map mapa){
        OnMove += robot.move;

        do {
            Console.WriteLine("Enter the command: ");
            //string command = Console.ReadLine();

            mapa.show();
            ConsoleKeyInfo command = Console.ReadKey(true);
            Console.Clear();

            switch (command.Key.ToString())
            {
                case "W":
                case "S" :
                case "D" :
                case "A" : OnMove(command.Key.ToString()[0]); break;
                case "G" : robot.collect(mapa.getNeighborhood(robot.X, robot.Y));break;
                case "O" : return true; //Debug, instawin
                case "Escape" : return false;
                default: Console.WriteLine(command.Key.ToString()); break;
            }

        } while (!mapa.isDone());

        OnMove -= robot.move;

        return true;
    }

    
}
