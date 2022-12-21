using System.Drawing;

public class Eva : Player
{
    public Eva(PointF location) : 
        base(location, Color.Purple, Color.White, "Eva") { }

    int searchindex = 0;
    int frame = 0;
    int points = 0;
    int i = 0;
    PointF? enemy = null;
    bool isloading = false;   
    protected override void loop()
    {
        frame++;
if (EnemiesInInfraRed.Count > 0)
        {
            enemy = EnemiesInInfraRed[0];
        }
        else
        {
            enemy = null;
        }
        if (Energy < 10)
        {
            StopMove();
            isloading = true;
            enemy = null;
        }
        if (isloading)
        {
            if (Energy > 40)
                isloading = false;
            else return;
        }
        if (enemy == null && Energy > 10)
            InfraRedSensor(5f * i++);
        else if (enemy != null && Energy > 5)
        {
            InfraRedSensor(enemy.Value);
            float dx = enemy.Value.X - this.Location.X,
                  dy = enemy.Value.Y - this.Location.Y;
            if (dx*dx + dy*dy >= 300f*300f)
                StartMove(enemy.Value);
            else
            {
                StopMove();
                if (i++ % 3 == 0)
                    Shoot(enemy.Value);
            }
        }
        if (EntitiesInStrongSonar == 0)
        {
            StrongSonar();
            points = Points;
        }
        else if (EntitiesInAccurateSonar.Count == 0 && EnemiesInInfraRed.Count == 0)
        {
            AccurateSonar();
        }
        else if (FoodsInInfraRed.Count == 0 && EntitiesInAccurateSonar.Count != 0)
        {
            InfraRedSensor(EntitiesInAccurateSonar[searchindex++ % EntitiesInAccurateSonar.Count]);
        }
        else
        {
            StartMove(FoodsInInfraRed[0]);
            if (Points != points)
            {
                StartTurbo();
                StrongSonar();
                StopMove();
                ResetInfraRed();
                ResetSonar();
            }
        }





        
        
    }
}