using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TowerDefenseOOP
{
    class Input
    {

        public static void LoadInput(int level)
        {
            string s = "Input" + level.ToString() +".txt";
            FileStream stream = new FileStream(s, FileMode.Open);   //Mở file Map.txt để đọc
            StreamReader reader = new StreamReader(stream, Encoding.ASCII);
            string buf;
            //Điểm số bắt đầu
            buf = (string)reader.ReadLine();
            Container.startingScore = Int32.Parse(buf);
            //Số lượng enemy trong mỗi lượt tấn công
            buf = (string)reader.ReadLine();
            Container.enemyPerWave = Int32.Parse(buf);
            //Thời gian giữa các đợt tấn công, tính theo miliseconds
            buf = (string)reader.ReadLine();
            Container.timeBetweenWave = float.Parse(buf);
            //Số lượng các đợt tấn công
            buf = (string)reader.ReadLine();
            Container.waveQuantity = Int32.Parse(buf);
            //Load thông tin từng đợt tấn công
            int i = Container.waveQuantity;
            while(i>0)
            {
                buf = reader.ReadLine();
                string[] a = buf.Split(' ');
                int[] enemy = new int[7];
                for (int j = 0; j < a.Length;j++ )
                {
                    if(j%2==0)
                    {
                        enemy[Int32.Parse(a[j])] = Int32.Parse(a[j + 1]);
                    }
                }
                Container.enemyEachWave.Add(enemy);
                i--;
            }
            reader.Close();
            stream.Close();
        }
    }
}
