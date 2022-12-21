using System.Drawing;

public class Adão : Player
{
    public Adão(PointF location) : 
        base(location, Color.Red, Color.Purple, "Adão") { }

    int i = 0;
    PointF? enemy = null;
    bool isloading = false;
    int searchindex = 0;
    int frame = 0;
    int points = 0;

    protected override void loop()
    {
        StartTurbo();
        if (EnemiesInInfraRed.Count > 0)
        {
            enemy = EnemiesInInfraRed[0];
        }
        else
        {
            enemy = null;
        }
        if (Energy < 50)
        {
            StopMove();
            isloading = true;
            enemy = null;
        }
        if (Life < 100)
        {
            StartMove(34 + 360 * 2);
        }
        if (isloading)
        {
            if (Energy > 100)
                isloading = false;
            else return;
        }
        if (enemy == null && Energy > 10)
            InfraRedSensor(5f * i++);
        else if (enemy != null && Energy > 10)
        {
            InfraRedSensor(enemy.Value);
            float dx = enemy.Value.X - this.Location.X,
                  dy = enemy.Value.Y - this.Location.Y;
            if (dx*dx + dy*dy >= 300f*300f)
                StartMove(enemy.Value);
            else
            {
                StopMove();
                if (i++ % 5 == 0)
                    Shoot(enemy.Value);
            }
        }

        frame++;
        if (Energy < 10 || frame % 10 == 0)
            return;
        if (EntitiesInStrongSonar == 0)
        {
            StrongSonar();
            points = Points;
        }
        else if (EntitiesInAccurateSonar.Count == 0)
        {
            AccurateSonar();
        }
        else if (FoodsInInfraRed.Count == 0)
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