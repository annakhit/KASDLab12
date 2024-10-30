using System;
using System.IO;
using System.Reflection;

namespace KASDLab12
{
    internal class Program
    {
        readonly static string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        readonly static string pathToFile = directory + @"\log.txt";
        static void Main()
        {
            Random random = new Random();
            StreamWriter streamWriter = new StreamWriter(pathToFile);
            MyPriorityQueue<Task> queue = new MyPriorityQueue<Task>();

            int iterations = Convert.ToInt32(Console.ReadLine());
            int number = 1;
            int step = 0;

            MyPriotiryTask<Task> maxTask = default;
            int maxTime = 0;

            //генерация
            streamWriter.WriteLine("ADD number priority step");
            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < random.Next(1, 10); j++)
                {
                    int priority = random.Next(1, 5);
                    queue.Add(new Task(step, number), priority);
                    streamWriter.WriteLine("ADD {0} {1} {2}", number, priority, step);
                    number++;
                }

                MyPriotiryTask<Task> task = queue.Pull();

                if (step - task.value.created > maxTime)
                {
                    maxTime = step - task.value.created;
                    maxTask = task;
                }

                streamWriter.WriteLine("REMOVE {0} {1} {2}", task.value.number, task.priority, step);

                step++;
            }

            //продолжение шагов, только удаление
            while (queue.Size() > 0)
            {
                MyPriotiryTask<Task> task = queue.Pull();

                if (step - task.value.created > maxTime)
                {
                    maxTime = step - task.value.created;
                    maxTask = task;
                }

                streamWriter.WriteLine("REMOVE {0} {1} {2}", task.value.number, task.priority, step);
                step++;
            }

            streamWriter.Close();

            Console.WriteLine("Max Task {0} Time {1} Created {2} Priority {3}",
                maxTask.value.number, maxTime, maxTask.value.created, maxTask.priority);

            Console.ReadKey();
        }
    }
}
