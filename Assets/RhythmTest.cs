using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using System.Text.RegularExpressions;

public class RhythmTest : MonoBehaviour {

    public KMAudio Audio;
    public KMBombInfo Bomb;
    public KMBombModule Module;

    public KMSelectable Button;
    public TextMesh Text;
    public TextMesh Late;
    public AudioSource Sounds;
    public AudioClip[] WhatPlays;
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

    bool NotReady = true;
    int Beat = 8;
    int Resetting;
    int CurrentTest = 1;
    bool Hittable;
    bool Press;
    bool TwitchPlaysCompatibility;
    int TPBonus;
    bool Prepped;
    bool Playing;
    bool LatencyComp;
    float Latency;
    bool Paused;
    bool InvalidPress;
    int Leniency = 2;
    bool DoubStrike;

    private static int moduleIdCounter = 1;
    private int moduleId;
    private bool ModuleSolved;

    void Awake()
    {
        moduleId = moduleIdCounter++;
        Button.OnInteract += delegate () { Pressed(); return false; };
        Text.characterSize = 0.3f;
        Text.text = "Rhythm Test";
    }
    void Pressed()
    {
        Sounds.PlayOneShot(WhatPlays[2]);
        Button.AddInteractionPunch(0.2f);
        if (!ModuleSolved)
        {
            if (LatencyComp)
                LatencyComp = false;
            else if (NotReady)
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
            else if (!TwitchPlaysCompatibility)
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
        }
    }

    void Start()
    {
        Debug.LogFormat("[Rhythm Test #{0}] Welcome to the rhythm test! Let's see how good your rhythm really is. Good luck!", moduleId);
    }

    IEnumerator TwitchPlaysImportance()
    {
        Text.characterSize = 1f;
        for (int i = 10; i > 0; i = i - 1)
        {
            Text.text = i.ToString();
            Sounds.PlayOneShot(WhatPlays[0]);
            yield return new WaitForSeconds(1f);
        }
        Text.text = "0";
        Sounds.PlayOneShot(WhatPlays[1]);
        LatencyComp = true;
        Prepped = true;
        while (LatencyComp)
        {
            yield return new WaitForSeconds(0.01f);
            Latency += 0.02f;
        }
        Text.characterSize = 0.7f;
        Text.text = Latency.ToString();
        yield return new WaitForSeconds(5f);
        StartCoroutine(ItsTestTime());
    }

    IEnumerator ItsTestTime()
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
            Playing = true;
            i = CurrentTest;
            Hittable = false;
            for (int j = 0; j < 7; j++)
            {
                Beat = Beat - 1;
                    yield return new WaitForSeconds(0.5f);
                    switch (i)
                    {
                        case 1: Text.text = Beat.ToString(); Sounds.PlayOneShot(WhatPlays[0]); break;
                        case 2: Text.text = Beat.ToString(); Sounds.PlayOneShot(WhatPlays[0]); break;
                        case 3: Text.text = Beat.ToString(); Sounds.PlayOneShot(WhatPlays[0]); break;
                        case 4: if (Beat >= 2) { Text.text = Beat.ToString(); Sounds.PlayOneShot(WhatPlays[0]); } else Text.text = ""; break;
                        case 5: if (Beat >= 2) { Text.text = Beat.ToString(); Sounds.PlayOneShot(WhatPlays[0]); } else Text.text = ""; break;
                        case 6: if (Beat >= 3) { Text.text = Beat.ToString(); Sounds.PlayOneShot(WhatPlays[0]); } else Text.text = ""; break;
                        case 7: if (Beat >= 3) { Text.text = Beat.ToString(); Sounds.PlayOneShot(WhatPlays[0]); } else Text.text = ""; break;
                        case 8: if (Beat >= 4) { Text.text = Beat.ToString(); Sounds.PlayOneShot(WhatPlays[0]); } else Text.text = ""; break;
                        case 9: if (Beat >= 4) { Text.text = Beat.ToString(); Sounds.PlayOneShot(WhatPlays[0]); } else Text.text = ""; break;
                        case 10: if (Beat >= 5) { Text.text = Beat.ToString(); Sounds.PlayOneShot(WhatPlays[0]); } else Text.text = ""; break;
                        case 11: if (Beat >= 5) { Text.text = Beat.ToString(); Sounds.PlayOneShot(WhatPlays[0]); } else Text.text = ""; break;
                        case 12: if (Beat >= 6) { Text.text = Beat.ToString(); Sounds.PlayOneShot(WhatPlays[0]); } else Text.text = ""; break;
                        case 13: if (Beat >= 6) { Text.text = Beat.ToString(); Sounds.PlayOneShot(WhatPlays[0]); } else Text.text = ""; break;
                        case 14: if (Beat >= 7) { Text.text = Beat.ToString(); Sounds.PlayOneShot(WhatPlays[0]); } else Text.text = ""; break;
                        case 15: if (Beat >= 7) { Text.text = Beat.ToString(); Sounds.PlayOneShot(WhatPlays[0]); } else Text.text = ""; break;
                        case 16: Text.text = Beat.ToString(); Sounds.PlayOneShot(WhatPlays[0]); break;
                    }
                DoubStrike = false;
                Late.text = "Late";
                TheLight.material = LightColors[0];
            }
            yield return new WaitForSeconds(0.35f);
            if (TwitchPlaysCompatibility)
            {
                yield return new WaitForSeconds(0.15f);
                Text.text = "0";
                Sounds.PlayOneShot(WhatPlays[1]);
                yield return new WaitForSeconds(Latency - 0.15f);
            }
            Hittable = true;
            Press = false;
            for (int j = 0; j < 9; j++)
            {
                if (j == 4)
                {
                    Text.text = "0";
                    Sounds.PlayOneShot(WhatPlays[1]);
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
                        TPBonus++;
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
                        TPBonus++;
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
                if (!TwitchPlaysCompatibility && !InvalidPress)
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
            CurrentTest = CurrentTest + 1;
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
    /*/
#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} prep (prepares the module for TP usage, REQUIRED TO START) | !{0} press (presses the button, the timing will be relative to your latency which is judged by '!{0} prep', will also reject command if unprepped) | !{0} pause (pauses/restarts current test, use only if lag spike occurs)";
#pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        TwitchPlaysCompatibility = true;
        string[] HellYeah = command.Split(' ');
        if (Regex.IsMatch(HellYeah[0], @"^\s*prep\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (HellYeah.Length != 1)
                yield return "sendtochaterror Too many arguments!";
            else
            {
                StartCoroutine(TwitchPlaysImportance());
            }
        }
        else if (Regex.IsMatch(HellYeah[0], @"^\s*press\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (HellYeah.Length != 1)
                yield return "sendtochaterror Too many arguments!";
            else if (!Prepped)
                yield return "sendtochaterror Please prep the module first!";
            else
                Button.OnInteract();
        }
        else if (Regex.IsMatch(HellYeah[0], @"^\s*pause\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (HellYeah.Length != 1)
                yield return "sendtochaterror Too many arguments!";
            else if (!Playing)
                yield return "sendtochaterror Please wait until playing!";
            else if (!Paused)
                Paused = true;
            else
            {
                Paused = false;
                LatencyComp = true;
                StartCoroutine(TwitchPlaysImportance());
            }
        }
        else
        {
            yield return null;
            yield return "sendtochaterror Invalid command. Please try again.";
        }
    }
    IEnumerator TwitchHandleForcedSolve()
    {
        yield return null;
        Module.HandlePass();
        Debug.LogFormat("[Rhythm Test #{0}] Autosolve command received.", moduleId);
    }
    /*/
}
