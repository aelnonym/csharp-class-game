using jewel_collector;
public class JewelCollector
{

    delegate void Move(char direction);
    delegate void KeyPress(string key);

    static event Move OnMove; 
    static event KeyPress OnKeyPressed;
    public static void Main() {
        
        // Crie Mapa Dimensao 10x10
        // Console.WriteLine("Creating Map");
        int w = 10;
        int h = 10;
        int level = 1;

        while(true){
            try {
                Map mapa = new Map(w, h, level);
                Robot player = new Robot(mapa);
                mapa.setPlayer(player, 0, 0);
                Console.Clear();
                
                bool Result = run(player, mapa);

                if(Result) {
                    w++;
                    h++;
                    level++;
                    if(w > 30){
                        Console.Clear();
                        Console.WriteLine("YOU WIN");
                        break;
                    }
                } else {
                    break;
                }
            }
            catch(RanOutOfEnergyException e) {
                Console.Clear();
                Console.WriteLine("Robot ran out of energy! Restarting!");
            }
            catch(StuckException e){
                Console.Clear();
                Console.WriteLine("Robot was stuck! Restarting!");
            }
        }
    }

    private static bool run(Robot robot, Map mapa){
        CheatWatcher watcher = new CheatWatcher(robot);
        OnMove += robot.move;
        OnKeyPressed += watcher.registerKey;

        do {
            if(!robot.HasEnergy()){
                OnMove -= robot.move;
                OnKeyPressed -= watcher.registerKey;
                throw new RanOutOfEnergyException();
            } 

            mapa.show();
            Console.WriteLine("Waiting for command... ");
            Console.WriteLine("Move:  [W] Up | [A] Left | [S] Down | [D] Right");
            Console.WriteLine("Other: [G] Collect Jewel / Interact | [R] Restart Map");
            ConsoleKeyInfo command = Console.ReadKey(true);
            OnKeyPressed(command.Key.ToString());
            Console.Clear();

            switch (command.Key.ToString())
            {
                case "W":
                case "S" :
                case "D" :
                case "A" : OnMove(command.Key.ToString()[0]); break;
                case "G" : robot.collect(mapa.getNeighborhood(robot.X, robot.Y)); break;
                case "O" : if (!robot.checkCheatMove(command.Key.ToString())) {Console.WriteLine(command.Key.ToString()); break;} else return true; //Debug, instawin
                case "R" : OnMove -= robot.move; OnKeyPressed -= watcher.registerKey; throw new StuckException();
                case "Escape" : return false;
                default: Console.WriteLine(command.Key.ToString()); break;
            }

        } while (!mapa.isDone());

        OnMove -= robot.move;
        OnKeyPressed -= watcher.registerKey;

        return true;
    }
    
}
