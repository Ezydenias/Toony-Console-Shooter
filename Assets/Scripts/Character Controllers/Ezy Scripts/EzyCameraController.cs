using UnityEngine;

public class EzyCameraController : CameraController
{
    // Start is called before the first frame update
    protected override void playerRotation(float horizontal)
    {
        if (target.GetComponent<EzyController>().getOnLadder())
        {
            target.transform.rotation = Quaternion.Lerp(target.transform.rotation,
                Quaternion.Euler(0, target.GetComponent<PlayerController>().getLadderOrientation().eulerAngles.y, 0),
                ladderFacingSmoothing);
        }
        else if (target.GetComponent<EzyController>().getShoving())
        {
            //do nothing
        }
        else
        {
            if (invertX)
                target.transform.Rotate(0, -horizontal * Time.deltaTime, 0);
            else
                target.transform.Rotate(0, horizontal * Time.deltaTime, 0);
        }
    }
}