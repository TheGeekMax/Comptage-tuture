using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour{
    const float ORANGE = 5f;

    [Header("texts")]
    public TextMeshProUGUI t1Text;
    public TextMeshProUGUI t2Text;
    public TextMeshProUGUI t1Count;
    public TextMeshProUGUI t2Count;

    [Header("hold")]
    public TextMeshProUGUI hold1;
    public TextMeshProUGUI hold2;
    public TextMeshProUGUI hold3;
    public TextMeshProUGUI hold4;
    public TextMeshProUGUI hold5;

    public TextMeshProUGUI med;
    //variable utilitaire
    int step = -1; // 0 - vert pour route 1; 1- vert pour route 2

    float t1 = 0f;
    float t2 = 0f;

    float vCount1 =0;
    float vCount2 =0;

    //tableau de 5 str
    string[] hold = new string[5];
    float[] hold1Count = new float[5];
    float[] hold2Count = new float[5];
    float[] hold1Time = new float[5];
    float[] hold2Time = new float[5];

    void Start(){
        //on initialise les textes
        for(int i = 0; i < 5; i++){
            hold[i] = "v1: 0s;0v .. v2: 0s;0v";
            hold1Count[i] = -1f;
            hold2Count[i] = -1f;
            hold1Time[i] = -1f;
            hold2Time[i] = -1f;
        }
        //on update les textes
        UpdateAllText();
    }

    void Update(){
        if(step == 0){
            //on ajoute dt a t1
            t1 += Time.deltaTime;
            //on affiche le temps
            t1Text.text = "T1 : " + t1.ToString("F2");
        }else if(step == 1){
            //on ajoute dt a t2
            t2 += Time.deltaTime;
            //on affiche le temps
            t2Text.text = "T2 : " + t2.ToString("F2");
        }
    }

    public void AddCar(){
        if(step == 0){
            vCount1++;
            //on affiche le nombre de voiture
            t1Count.text = "C1 : " + vCount1.ToString();
        }else if(step == 1){
            vCount2++;
            //on affiche le nombre de voiture
            t2Count.text = "C2 : " + vCount2.ToString();
        }
    }

    public void RemoveCar(){
        if(step == 0){
            vCount1--;
            //on affiche le nombre de voiture
            t1Count.text = "C1 : " + vCount1.ToString();
        }else if(step == 1){
            vCount2--;
            //on affiche le nombre de voiture
            t2Count.text = "C2 : " + vCount2.ToString();
        }
    }

    string CalculateMedianStr(){
        int c = 0;
        float t1M = 0f;
        float t2M = 0f;
        float v1M = 0f;
        float v2M = 0f;
        for(int i = 0; i < 5; i++){
            if(hold1Count[i] != -1){
                c++;
                t1M += hold1Time[i];
                t2M += hold2Time[i];
                v1M += hold1Count[i];
                v2M += hold2Count[i];
            }
        }
        if(c == 0){
            return "v1: 0s;0v .. v2: 0s;0v";
        }
        t1M /= c;
        t2M /= c;
        v1M /= c;
        v2M /= c;
        return "med, v1: " + t1M.ToString("F2") + "s;" + v1M.ToString("F2") + "v .. v2: " + t2M.ToString("F2") + "s;" + v2M.ToString("F2") + "v";
    }

    string CalculateValue(int ind){
        if(hold1Count[ind] == -1){
            return "v1: 0s;0v .. v2: 0s;0v";
        }
        return "exp n°" + (ind +1).ToString() +", v1: " + hold1Time[ind].ToString("F2") + "s;" + hold1Count[ind].ToString("F2") + "v .. v2: " + hold2Time[ind].ToString("F2") + "s;" + hold2Count[ind].ToString("F2") + "v";
    }

    void AppendData(){
        //on decale tout d'un crant, sans faire gaffe au dernier
        for(int i = 4; i > 0; i--){
            hold1Count[i] = hold1Count[i-1];
            hold2Count[i] = hold2Count[i-1];
            hold1Time[i] = hold1Time[i-1];
            hold2Time[i] = hold2Time[i-1];
        }

        //on ajoute les données reecente en pos 0
        hold1Count[0] = vCount1/(t1 - ORANGE);
        hold2Count[0] = vCount2/(t2 - ORANGE);
        hold1Time[0] = t1 - ORANGE;
        hold2Time[0] = t2 - ORANGE;
    }

    void UpdateAllText(){
        t1Text.text = "T1 : " + t1.ToString("F2");
        t2Text.text = "T2 : " + t2.ToString("F2");
        t1Count.text = "C1 : " + vCount1.ToString();
        t2Count.text = "C2 : " + vCount2.ToString();

        hold1.text = CalculateValue(0);
        hold2.text = CalculateValue(1);
        hold3.text = CalculateValue(2);
        hold4.text = CalculateValue(3);
        hold5.text = CalculateValue(4);

        med.text = CalculateMedianStr();
    }

    public void ChangeStep(){
        if(step == 0){
            step = 1;
        }else if(step == 1){
            step = 0;
            //on append les donnees
            AppendData();
            //on reset les compteurs
            vCount1 = 0;
            vCount2 = 0;
            t1 = 0f;
            t2 = 0f;

            UpdateAllText();
        }else if(step == -1){
            step = 0;
        }
    }
}
