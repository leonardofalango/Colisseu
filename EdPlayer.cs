using System.Drawing;

public class EdPlayer : Player
{
    public EdPlayer(PointF location) : 
        base(location, Color.Red, Color.Blue, "Edjalma") { }

    int searchindex = 0;
    int frame = 0;
    int points = 0;
    protected override void loop()
    {
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