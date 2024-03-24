using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[XmlRoot("QuestionAndAnswers")]
public class QuestionAndAnswersHolder
{
    //    [XmlElement("QuestionAndAnswers")]
    //    public List<QuestionsAndAnswers> QnA = new List<QuestionsAndAnswers>();

    [XmlElement("QuestionSet")]
    public List<QuestionsAndAnswers> QnA;
}
