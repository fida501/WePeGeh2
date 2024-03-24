using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
public class QuestionsAndAnswers
{
    [XmlElement("Question")]
    public string Question;

    [XmlArray("Answers")]
    [XmlArrayItem("Answer")]
    public string[] Answers;

    [XmlElement("CorrectAnswerIndex")]
    public int CorrectAnswers;
}


//[System.Serializable]
/*[System.Serializable ,XmlRoot("QuestionAndAnswers")]
public class QuestionsAndAnswers
{
    [XmlElement("Question")]
    public string Question;
    [XmlArray("Answers")]
    [XmlArrayItem("Answer")]
    public string[] Answers;
    [XmlElement("CorrectAnswer")]
    public int CorrectAnswers;
}*/

class cobaClass {
    public string Questions { get; private set; }

    public string[] Answers { get; private set; }

    public int CorrectAnswers { get; private set; } 


}
