using jewel_collector;
public class JewelCollector
{
    public static void Main() {
        bool running = true;
         // Crie Mapa Dimensao 10x10
        Console.WriteLine("Creating Map");
        Map mapa = new Map(10, 10);

        // Crie e Insira Joias
        mapa.insertProp(new Jewel("RED"), 1, 9);
        mapa.insertProp(new Jewel("RED"), 8, 8);
        mapa.insertProp(new Jewel("GREEN"), 9, 1);
        mapa.insertProp(new Jewel("GREEN"), 7, 6);
        mapa.insertProp(new Jewel("BLUE"), 3, 4);
        mapa.insertProp(new Jewel("BLUE"), 2, 1);

        // Crie e insira Obstaculos
        foreach(int i in Enumerable.Range(0, 7)){
            mapa.insertProp(new Obstacle("WATER"), 5, i);
        }

        mapa.insertProp(new Obstacle("TREE"), 5, 9);
        mapa.insertProp(new Obstacle("TREE"), 3, 9);
        mapa.insertProp(new Obstacle("TREE"), 8, 3);
        mapa.insertProp(new Obstacle("TREE"), 2, 5);
        mapa.insertProp(new Obstacle("TREE"), 1, 4);

        // Crie o Robo
        Robot player = new Robot();
        mapa.setPlayer(player, 0, 0);

        do {
            Console.WriteLine("Enter the command: ");
            string command = Console.ReadLine();

            if (command.Equals("quit")) {
                running = false;
            } else if (command.Equals("w") || command.Equals("a") || command.Equals("s") || command.Equals("d")) { 
                if(mapa.movePlayer(command[0]) != 0){
                    Console.WriteLine("Obstacle");
                }
                mapa.show();
            } else if (command.Equals("g")) { 
                mapa.show();
            }
        } while (running);
    }
}
