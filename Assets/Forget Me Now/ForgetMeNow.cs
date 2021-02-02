using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;

public class ForgetMeNow : MonoBehaviour {
    public KMAudio Audio;
    public KMBombInfo Bomb;
    public KMBombModule Module;

    public KMSelectable[] Buttons;
    public Renderer[] Lights;
    public Material[] LightColors;
    public GameObject[] Everything;
    public GameObject TheBigOne;
    
    public AudioClip[] Yes;
    public AudioClip[] No;

    int CorrectAnswer;
    int FirstFive;
    int SecondFive;
    int ThirdFive;
    int FourthFive;
    int FifthFive;
    int OnlyForCalculating;

    string Help;
    string TheAnswer;
    int YourNumber = 3;

    bool GoodToGo;
    bool Started;

    public TextMesh[] ButtonTexts;
    public TextMesh[] Screens;

    public AudioSource SFX;
    public AudioClip[] Sounds;

    private static int moduleIdCounter = 1;
    private int moduleId;
    
    private void Awake() {
        moduleId = moduleIdCounter++;
        for (int i = 0; i < Buttons.Length; i++) {
            int j = i;
            Buttons[i].OnInteract += delegate () {
                ButtonPress(j);
                return false;
            };
        }
        Screens[0].text = "";
        Screens[1].text = "";
        Screens[2].text = "";
        CorrectAnswer = UnityEngine.Random.Range(1000, 10000);
        FirstFive = UnityEngine.Random.Range(10000, 100000);
        SecondFive = UnityEngine.Random.Range(10000, 20000);
        ThirdFive = UnityEngine.Random.Range(10000, 100000);
        FourthFive = UnityEngine.Random.Range(10000, 100000);
        FifthFive = UnityEngine.Random.Range(10000, 100000);
        Debug.LogFormat("[More Math #{0}] The starting number here will be {1}.", moduleId, CorrectAnswer);
        StartCoroutine(TestingPurposes());
        CalculationOne();
    }

    private void ButtonPress(int i) {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, gameObject.transform);
        Buttons[i].AddInteractionPunch(0.2f);
        if (GoodToGo == true && YourNumber > 0)
        {
            Help += i.ToString();
            Screens[1].text = Help;
            YourNumber = YourNumber - 1;
            if (YourNumber == 0)
            {
                StartCoroutine(SolveCheck());
            }
        }
        else if (!Started)
        {
            Started = true;
            StartCoroutine(Startup());
        }
    }

    private void CalculationOne()
    {
        OnlyForCalculating = FirstFive - CorrectAnswer;
        Debug.LogFormat("[More Math #{0}] We subtract {2} from {1}. This results in {3}.", moduleId, FirstFive, CorrectAnswer, OnlyForCalculating);
        CalculationTwo();
    }

    private void CalculationTwo()
    {
        OnlyForCalculating = OnlyForCalculating*SecondFive;
        Debug.LogFormat("[More Math #{0}] Multiplying the earlier number with {1} gives us {2}.", moduleId, SecondFive, OnlyForCalculating);
        CalculationThree();
    }

    private void CalculationThree()
    {
        OnlyForCalculating = OnlyForCalculating - 7777;
        OnlyForCalculating = ThirdFive + OnlyForCalculating;
        Debug.LogFormat("[More Math #{0}] Subtracting 7777 from that and adding {1} equals {2}.", moduleId, ThirdFive, OnlyForCalculating);
        CalculationFour();
    }

    private void CalculationFour()
    {
        OnlyForCalculating = FourthFive * 10000 - OnlyForCalculating;
        OnlyForCalculating = OnlyForCalculating % 1000;
        if (OnlyForCalculating < 0)
            OnlyForCalculating = OnlyForCalculating + 1000;
        Debug.LogFormat("[More Math #{0}] Multiplying {1} by 10000, subtracting the above number from that, modulo 1000, our new number is {2}.", moduleId, FourthFive, OnlyForCalculating);
        CalculationFive();
    }

    private void CalculationFive()
    {
        OnlyForCalculating = FifthFive % OnlyForCalculating;
        if (OnlyForCalculating < 100)
            OnlyForCalculating = OnlyForCalculating + 100;
        Debug.LogFormat("[More Math #{0}] Finally, taking {1} modulo the above number, the final answer is {2}.", moduleId, FifthFive, OnlyForCalculating);
    }

    IEnumerator SolveCheck()
    {
        Screens[0].text = "";
        TheAnswer = Help;
        Debug.LogFormat("[More Math #{0}] Your submission is {1}.", moduleId, TheAnswer);
        for (int i = 0; i < 50; i++)
        {
            FirstFive = UnityEngine.Random.Range(10000, 100000);
            SecondFive = UnityEngine.Random.Range(10000, 100000);
            ThirdFive = UnityEngine.Random.Range(10000, 100000);
            FourthFive = UnityEngine.Random.Range(10000, 100000);
            FifthFive = UnityEngine.Random.Range(10000, 100000);
            if (i == 40)
                SFX.Play();
            Screens[2].text = FirstFive.ToString() + System.Environment.NewLine + SecondFive.ToString() + System.Environment.NewLine + ThirdFive.ToString() + System.Environment.NewLine + FourthFive.ToString() + System.Environment.NewLine + FifthFive.ToString();
            yield return new WaitForSeconds(0.02f);
        }
        Help = Help.Substring(0, Help.Length - 1);
        Screens[1].text = Help;
        for (int i = 0; i < 50; i++)
        {
            FirstFive = UnityEngine.Random.Range(10000, 100000);
            SecondFive = UnityEngine.Random.Range(10000, 100000);
            ThirdFive = UnityEngine.Random.Range(10000, 100000);
            FourthFive = UnityEngine.Random.Range(10000, 100000);
            FifthFive = UnityEngine.Random.Range(10000, 100000);
            if (i == 40)
                SFX.Play();
            Screens[2].text = FirstFive.ToString() + System.Environment.NewLine + SecondFive.ToString() + System.Environment.NewLine + ThirdFive.ToString() + System.Environment.NewLine + FourthFive.ToString() + System.Environment.NewLine + FifthFive.ToString();
            yield return new WaitForSeconds(0.02f);
        }
        Help = Help.Substring(0, Help.Length - 1);
        Screens[1].text = Help;
        for (int i = 0; i < 50; i++)
        {
            FirstFive = UnityEngine.Random.Range(10000, 100000);
            SecondFive = UnityEngine.Random.Range(10000, 100000);
            ThirdFive = UnityEngine.Random.Range(10000, 100000);
            FourthFive = UnityEngine.Random.Range(10000, 100000);
            FifthFive = UnityEngine.Random.Range(10000, 100000);
            if (i == 40)
                SFX.Play();
            Screens[2].text = FirstFive.ToString() + System.Environment.NewLine + SecondFive.ToString() + System.Environment.NewLine + ThirdFive.ToString() + System.Environment.NewLine + FourthFive.ToString() + System.Environment.NewLine + FifthFive.ToString();
            yield return new WaitForSeconds(0.02f);
        }
        Help = Help.Substring(0, Help.Length - 1);
        Screens[1].text = Help;
        for (int i = 0; i < 50; i++)
        {
            FirstFive = UnityEngine.Random.Range(10000, 100000);
            SecondFive = UnityEngine.Random.Range(10000, 100000);
            ThirdFive = UnityEngine.Random.Range(10000, 100000);
            FourthFive = UnityEngine.Random.Range(10000, 100000);
            FifthFive = UnityEngine.Random.Range(10000, 100000);
            if (i == 40)
                SFX.Play();
            Screens[2].text = FirstFive.ToString() + System.Environment.NewLine + SecondFive.ToString() + System.Environment.NewLine + ThirdFive.ToString() + System.Environment.NewLine + FourthFive.ToString() + System.Environment.NewLine + FifthFive.ToString();
            yield return new WaitForSeconds(0.02f);
        }
        if (OnlyForCalculating.ToString() == TheAnswer)
        {
            Debug.LogFormat("[More Math #{0}] Correct! Module solved! Celebration time!", moduleId);
            Screens[1].text = "YES";
            Screens[0].text = "";
            Screens[2].text = "";
            GoodToGo = false;
            Module.HandlePass();
            StartCoroutine(Yep());
            yield return new WaitForSeconds(2f);
            Screens[1].text = "YE";
            yield return new WaitForSeconds(0.65f);
            Screens[1].text = "Y";
            yield return new WaitForSeconds(0.65f);
            Screens[1].text = "";
        }
        else
        {
            Debug.LogFormat("[More Math #{0}] ...Which, unfortunately, is wrong. Sorry, but I gotta strike ya and reset this thing.", moduleId);
            Module.HandleStrike();
            Screens[1].text = "NO";
            Screens[2].text = "WRONG";
            SecondFive = UnityEngine.Random.Range(10000, 20000);
            CorrectAnswer = UnityEngine.Random.Range(1000, 10000);
            CalculationOne();
            StartCoroutine(Nope());
        }
    }

    IEnumerator TestingPurposes()
    {
        for (int j = 0; j < Buttons.Length; j++)
        {
            Buttons[j].GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
        for (int j = 0; j < Buttons.Length; j++)
        {
            Buttons[j].GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(TestingPurposes());
    }

    IEnumerator Startup()
    {
        if (!GoodToGo)
        {
            Debug.LogFormat("[More Math #{0}: Alright... let's do this.", moduleId);
            SFX.clip = Sounds[0];
            SFX.Play();
            yield return new WaitForSeconds(0.3f);
            Screens[0].text = "WELC";
            Screens[1].text = "OME";
            yield return new WaitForSeconds(2.25f);
            Screens[2].text = Starting[UnityEngine.Random.Range(0, Starting.Length)];
            yield return new WaitForSeconds(1.65f);
            Screens[0].text = "";
            Screens[1].text = "";
            Screens[2].text = "";
            yield return new WaitForSeconds(0.4f);
        }
        else
            GoodToGo = false;
        SFX.clip = Sounds[1];
        SFX.Play();
        yield return new WaitForSeconds(0.2f);
        Screens[1].text = "NO";
        Screens[0].text = CorrectAnswer.ToString();
        yield return new WaitForSeconds(2f);
        Screens[2].characterSize = 10f;
        SFX.Play();
        yield return new WaitForSeconds(0.2f);
        Screens[2].text = FirstFive.ToString();
        yield return new WaitForSeconds(2f);
        SFX.Play();
        yield return new WaitForSeconds(0.2f);
        Screens[2].text += System.Environment.NewLine + SecondFive.ToString();
        yield return new WaitForSeconds(2f);
        SFX.Play();
        yield return new WaitForSeconds(0.2f);
        Screens[2].text += System.Environment.NewLine + ThirdFive.ToString();
        yield return new WaitForSeconds(2f);
        SFX.Play();
        yield return new WaitForSeconds(0.2f);
        Screens[2].text += System.Environment.NewLine + FourthFive.ToString();
        yield return new WaitForSeconds(2f);
        SFX.Play();
        yield return new WaitForSeconds(0.2f);
        Screens[2].text += System.Environment.NewLine + FifthFive.ToString();
        yield return new WaitForSeconds(2f);
        SFX.Play();
        yield return new WaitForSeconds(0.2f);
        Screens[1].text = "";
        YourNumber = 3;
        GoodToGo = true;
    }

    IEnumerator Nope()
    {
        yield return new WaitForSeconds(0.01f);
        SFX.clip = No[UnityEngine.Random.Range(0, No.Length)];
        SFX.Play();
        while (SFX.isPlaying)
        {
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(Startup());
    }

    IEnumerator Yep()
    {
        yield return new WaitForSeconds(0.01f);
        for (int i = 0; i < ButtonTexts.Length; i++)
            ButtonTexts[i].text = "";
        SFX.clip = Yes[0];
        SFX.Play();
        for (int j = 0; j < Everything.Length; j++)
        {
            Everything[j].GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.025f);
        }
        yield return new WaitForSeconds(7.1f);
        var trans = 1f;
        var col = TheBigOne.GetComponent<Renderer>().material.color;
        for (int i = 0; i < 100; i++)
        {
            trans = trans - 0.01f;
            col.a = trans;
            var newColor = new Color(col.r, col.g, col.b, trans);
            TheBigOne.GetComponent<Renderer>().material.color = newColor;
            yield return new WaitForSeconds(0.015f);
        }
    }

    private static readonly string[] Starting =
    {
        "\"[insert\ninspiratio-\n-nal quote\nhere]\"",
        "\"HELP I'M\nTRAPPED IN\nA MODULE\"",
        "\"Bet you\ncan't guess\nwhat this\nsound is\nfrom.\"",
        "\"Uhhhhhhhhhh\nhhhhhhhhhhh\nhhhhhhhhhhh\nhhhhhhhhhhh\nhhhhhhhhhhh\nhhhhhhhhhhh\"",
        "\"Talk about\na low\nbudget\nflight!\"",
        "\"I knew\nI should\nhave\ninvited my\ncousin,\nFalco\nLombardi!\"",
        "\"I can't\nthink of\nany more\nnumbers...\"",
        "\"Isn't this\nsomething\nhexOS\nalready\ndoes? You\nknow, the\nshuffling\ntext thing?\"",
        "\"How do any\nof these\nquotes make\na single\nounce of\nsense? Like\nseriously,\nHOW?!\"",
        "\"No, that's\nwrong!\"",
        "\"A body\nhas been\ndiscovered!\"",
        "\"This is so\nsad, Alexa\nplay\nDespacito\"",
        "\"I spent\nabout an\nhour just\non writing\nthe random\nquotes.\""
    };
}