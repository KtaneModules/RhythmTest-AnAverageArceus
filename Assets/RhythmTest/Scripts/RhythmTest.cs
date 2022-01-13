using System.Collections;
using UnityEngine;

public class RhythmTest : MonoBehaviour
{

    public KMAudio Audio;
    public KMBombInfo Bomb;
    public KMBombModule Module;

    public KMSelectable Button;
    public TextMesh Text;
    public TextMesh Late;
    public Material[] LightColors;
    public Renderer[] Test1;
    public Renderer[] Test2;
    public Renderer[] Test3;
    public Renderer[] Test4;
    public Renderer[] Test5;
    public Renderer[] Test6;
    public Renderer[] Test7;
    public Renderer[] Test8;
    public Renderer[] Test9;
    public Renderer[] Test10;
    public Renderer[] Test11;
    public Renderer[] Test12;
    public Renderer[] Test13;
    public Renderer[] Test14;
    public Renderer[] Test15;
    public Renderer[] Test16;
    public Renderer TheLight;

    private bool NotReady = true;
    private int Beat = 8;
    private bool Hittable;
    private bool Press;
    private bool InvalidPress;
    private int Leniency = 2;
    private bool DoubStrike;

    private static int moduleIdCounter = 1;
    private int moduleId;
    private bool ModuleSolved;

    private void Start()
    {
        moduleId = moduleIdCounter++;
        Button.OnInteract += Pressed();
        Text.characterSize = 0.3f;
        Text.text = "Rhythm Test";
        Debug.LogFormat("[Rhythm Test #{0}] Welcome to the rhythm test! Let's see how good your rhythm really is. Good luck!", moduleId);
    }

    private KMSelectable.OnInteractHandler Pressed()
    {
        return delegate
        {
            if (ModuleSolved)
                return false;

            Audio.PlaySoundAtTransform("Click", Button.transform);
            Button.AddInteractionPunch(0.2f);

            if (NotReady)
            {
                Text.characterSize = 0.7f;
                StartCoroutine(ItsTestTime());
            }
            else if (Hittable)
            {
                Press = true;
                Hittable = false;
            }
            else if (DoubStrike)
            {
                Late.text = "Very Late";
                Debug.LogFormat("[Rhythm Test #{0}] In order to not give unfair penalties, no strike was given on your press being just barely too late.", moduleId);
            }
            else
            {
                if (Leniency == 2)
                {
                    Leniency = 1;
                    Debug.LogFormat("[Rhythm Test #{0}] You pressed the button when the module wasn't anticipating it, but this is your first miss, so no strike was given.", moduleId);
                }
                else if (Leniency == 1)
                {
                    Leniency = 0;
                    Debug.LogFormat("[Rhythm Test #{0}] Careful now, you're out of free passes! (Button pressed at invalid time)", moduleId);
                }
                else
                {
                    Module.HandleStrike();
                    InvalidPress = true;
                    Debug.LogFormat("[Rhythm Test #{0}] That last press wasn't even close to accurate and you don't have any Get Out Of Jail Free cards! STRIIIIIKE!", moduleId);
                }
            }
            return false;
        };
    }

    private IEnumerator ItsTestTime()
    {
        NotReady = false;
        Text.text = "READY";
        yield return new WaitForSeconds(3f);
        Text.characterSize = 1f;
        Text.text = "";
        yield return new WaitForSeconds(2f);
        for (int i = 1; i < 17; i++)
        {
            InvalidPress = false;
            Hittable = false;
            for (int j = 0; j < 7; j++)
            {
                Beat = Beat - 1;
                yield return new WaitForSeconds(0.5f);
                switch (i)
                {
                    case 1: case 2: case 3: case 16: Text.text = Beat.ToString(); Audio.PlaySoundAtTransform("Beep", Button.transform); break;
                    case 4: case 5: if (Beat >= 2) { Text.text = Beat.ToString(); Audio.PlaySoundAtTransform("Beep", Button.transform); } else Text.text = ""; break;
                    case 6: case 7: if (Beat >= 3) { Text.text = Beat.ToString(); Audio.PlaySoundAtTransform("Beep", Button.transform); } else Text.text = ""; break;
                    case 8: case 9: if (Beat >= 4) { Text.text = Beat.ToString(); Audio.PlaySoundAtTransform("Beep", Button.transform); } else Text.text = ""; break;
                    case 10: case 11: if (Beat >= 5) { Text.text = Beat.ToString(); Audio.PlaySoundAtTransform("Beep", Button.transform); } else Text.text = ""; break;
                    case 12: case 13: if (Beat >= 6) { Text.text = Beat.ToString(); Audio.PlaySoundAtTransform("Beep", Button.transform); } else Text.text = ""; break;
                    case 14: case 15: if (Beat >= 7) { Text.text = Beat.ToString(); Audio.PlaySoundAtTransform("Beep", Button.transform); } else Text.text = ""; break;
                }
                DoubStrike = false;
                Late.text = "Late";
                TheLight.material = LightColors[0];
            }
            yield return new WaitForSeconds(0.35f);
            Hittable = true;
            Press = false;
            for (int j = 0; j < 9; j++)
            {
                if (j == 4)
                {
                    Text.text = "0";
                    Audio.PlaySoundAtTransform("Hit", Button.transform);
                }
                if (!Press && Hittable && (i < 5))
                {
                    switch (i)
                    {
                        case 1: Test1[j].material = LightColors[1]; if (j == 5) Test1[j - 1].material = LightColors[2]; else if (j != 0) Test1[j - 1].material = LightColors[0]; yield return new WaitForSeconds(0.025f); break;
                        case 2: Test2[j].material = LightColors[1]; if (j == 5) Test2[j - 1].material = LightColors[2]; else if (j != 0) Test2[j - 1].material = LightColors[0]; yield return new WaitForSeconds(0.025f); break;
                        case 3: Test3[j].material = LightColors[1]; if (j == 5) Test3[j - 1].material = LightColors[2]; else if (j != 0) Test3[j - 1].material = LightColors[0]; yield return new WaitForSeconds(0.025f); break;
                        case 4: Test4[j].material = LightColors[1]; if (j == 5) Test4[j - 1].material = LightColors[2]; else if (j != 0) Test4[j - 1].material = LightColors[0]; yield return new WaitForSeconds(0.025f); break;
                    }
                }
                else if (!Press && Hittable && (i < 5) && (j == 8))
                {
                    switch (i)
                    {
                        case 1: Test1[8].material = LightColors[0]; break;
                        case 2: Test2[8].material = LightColors[0]; break;
                        case 3: Test3[8].material = LightColors[0]; break;
                        case 4: Test4[8].material = LightColors[0]; break;
                    }
                }
                else if (Press && (i > 4))
                {
                    switch (i)
                    {
                        case 5: Test5[j].material = LightColors[1]; break;
                        case 6: Test6[j].material = LightColors[1]; break;
                        case 7: Test7[j].material = LightColors[1]; break;
                        case 8: Test8[j].material = LightColors[1]; break;
                        case 9: Test9[j].material = LightColors[1]; break;
                        case 10: Test10[j].material = LightColors[1]; break;
                        case 11: Test11[j].material = LightColors[1]; break;
                        case 12: Test12[j].material = LightColors[1]; break;
                        case 13: Test13[j].material = LightColors[1]; break;
                        case 14: Test14[j].material = LightColors[1]; break;
                        case 15: Test15[j].material = LightColors[1]; break;
                        case 16: Test16[j].material = LightColors[1]; break;
                    }
                    if (j < 4)
                        Debug.LogFormat("[Rhythm Test #{0}] Test {1}: You pressed a bit early...", moduleId, i);
                    else if (j == 4)
                    {
                        Debug.LogFormat("[Rhythm Test #{0}] Test {1}: You hit the beat perfectly!", moduleId, i);
                    }
                    else if (j > 4)
                        Debug.LogFormat("[Rhythm Test #{0}] Test {1}: You were a bit late...", moduleId, i);
                    Press = false;
                }
                else if (Press && (i < 5))
                {
                    if (j < 5)
                        Debug.LogFormat("[Rhythm Test #{0}] Test {1}: You pressed a bit early...", moduleId, i);
                    else if (j == 5)
                    {
                        Debug.LogFormat("[Rhythm Test #{0}] Test {1}: You hit the beat perfectly!", moduleId, i);
                    }
                    else if (j > 5)
                        Debug.LogFormat("[Rhythm Test #{0}] Test {1}: You were a bit late...", moduleId, i);
                    Press = false;
                    yield return new WaitForSeconds(0.025f);
                }
                else if (i > 4)
                    yield return new WaitForSeconds(0.025f);
            }
            if (Hittable && !Press)
            {
                if (!InvalidPress)
                {
                    if (Leniency == 2)
                    {
                        Leniency = 1;
                        Debug.LogFormat("[Rhythm Test #{0}] Test {1}: Button wasn't pressed, but this is your first miss, so no strike was given.", moduleId, i);
                    }
                    else if (Leniency == 1)
                    {
                        Leniency = 0;
                        Debug.LogFormat("[Rhythm Test #{0}] Test {1}: Careful now, you're out of free passes! (Button not pressed at all)", moduleId, i);
                    }
                    else
                    {
                        Module.HandleStrike();
                        TheLight.material = LightColors[1];
                        Debug.LogFormat("[Rhythm Test #{0}] Test {1}: Completely missed, and you're out of free passes! STRIIIIIKE!", moduleId, i);
                    }
                }
                else
                    Debug.LogFormat("[Rhythm Test #{0}] Test {1}: You didn't press the button near the right time.", moduleId, i);
                DoubStrike = true;
            }
            Beat = 8;
        }
        Text.text = "";
        yield return new WaitForSeconds(3f);
        Module.HandlePass();
        ModuleSolved = true;
        TheLight.material = LightColors[2];
        Debug.LogFormat("[Rhythm Test #{0}] Test complete! Congratulations.", moduleId);
        Text.characterSize = 0.7f;
        Text.text = "Done!";
    }
}
