using System.Collections;
using System.Linq;
using UnityEngine;

public class S_DateManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] int _charmAddGoodPresentation;
    [SerializeField] int _charmAddBadPresentation;

    //[Header("References")]

    [Header("RSE")]
    [SerializeField] RSE_GenerateQuestion _rseGenerateQuestion;
    [SerializeField] RSE_GenerateSpeech _rseGenerateSpeech;
    [SerializeField] RSE_GenerateQuestionSpeech _rseGenerateQuestionSpeech;

    [SerializeField] RSE_DelayGenerateQuestion _rseDelayGenerateQuestion;
    [SerializeField] RSE_DelayGenerateSpeech _rseDelayGenerateSpeech;
    [SerializeField] RSE_DelayGenerateSpeechQuestion _rseDelayGenerateSpeechQuestion;

    [SerializeField] RSE_UpdateCharm _rseUpdateCharm;

    [SerializeField] RSE_OnBadPresentation _OnBadPresentation;
    [SerializeField] RSE_OnGoodPresentation _OnGoodPresentation;

    [Header("RSO")]
    [SerializeField] RSO_CurrentProfile _rsoCurrentProfile;
    [SerializeField] RSO_CurrentDateStep _rsoCurrentDateStep;
    [SerializeField] RSO_CurrentListObject _rsoCurrentListObject;


    //[Header("SSO")]

    private void Start()
    {
        _rsoCurrentDateStep.Value = DateStep.Presentation;

        _rseDelayGenerateQuestion.action += GenerateQuestion;
        _rseDelayGenerateSpeech.action += GenerateSpeech;
        _rseDelayGenerateSpeechQuestion.action += GenerateSpeechQuestion;


        StartCoroutine(StartPresentation());
    }

    private void OnDestroy()
    {
        _rsoCurrentDateStep.Value = DateStep.Presentation;

        _rseDelayGenerateQuestion.action -= GenerateQuestion;
        _rseDelayGenerateSpeech.action -= GenerateSpeech;
        _rseDelayGenerateSpeechQuestion.action -= GenerateSpeechQuestion;
    }

    //Need id items to do 
    IEnumerator StartPresentation()
    {
        yield return new WaitForSeconds(2f);

        PresentationTcheck();

        yield return null;

        //_rsoCurrentDateStep.Value = DateStep.Starter;

        //GenerateSpeech();
    }

    void GenerateSpeech()
    {
        StartCoroutine(DelayGenerateSpeech());
    }
    void GenerateQuestion()
    {
        StartCoroutine(DelayGenerateQuestion());
    }

    void GenerateSpeechQuestion()
    {
        StartCoroutine(DelayGenerateSpeechQuestion());
    }

    IEnumerator DelayGenerateSpeech()
    {
        yield return new WaitForSeconds(2f);

        _rseGenerateSpeech.RaiseEvent();

        yield return null;
    }

    IEnumerator DelayGenerateQuestion()
    {
        yield return new WaitForSeconds(2f);

        _rseGenerateQuestion.RaiseEvent();

        yield return null;
    }

    IEnumerator DelayGenerateSpeechQuestion()
    {
        yield return new WaitForSeconds(2f);

        _rseGenerateQuestionSpeech.RaiseEvent();

        yield return null;
    }

    void PresentationTcheck()
    {
        if (_rsoCurrentProfile.Value.ProfilType == ProfilType.Street)
        {
            if(_rsoCurrentListObject.Value.Exists(item => item.Index == 14))
            {
                _rseUpdateCharm.RaiseEvent(_charmAddGoodPresentation);
            }
            if (_rsoCurrentListObject.Value.Exists(item => item.Index == 10))
            {
                _rseUpdateCharm.RaiseEvent(_charmAddBadPresentation);
            }




        }
        else if(_rsoCurrentProfile.Value.ProfilType == ProfilType.Babos)
        {
            if (_rsoCurrentListObject.Value.Exists(item => item.Index == 6))
            {
                _rseUpdateCharm.RaiseEvent(_charmAddGoodPresentation);
            }
            if (_rsoCurrentListObject.Value.Exists(item => item.Index == 8))
            {
                _rseUpdateCharm.RaiseEvent(_charmAddBadPresentation);
            }




        }
        else if(_rsoCurrentProfile.Value.ProfilType == ProfilType.Rich)
        {
            if (_rsoCurrentListObject.Value.Exists(item => item.Index == 11))
            {
                _rseUpdateCharm.RaiseEvent(_charmAddGoodPresentation);
            }
            if (_rsoCurrentListObject.Value.Exists(item => item.Index == 13))
            {
                _rseUpdateCharm.RaiseEvent(_charmAddBadPresentation);
            }




        }
    }

    void TtcheckDress(ProfilType profilTyep)
    {
        switch (profilTyep)
        {
            case ProfilType.Street:
                if (_rsoCurrentListObject.Value.Exists(item => item.Index == 0))
                {
                    _OnGoodPresentation.RaiseEvent("Nice style! I'm Kathleen, you are delicious.");

                }
                else if (_rsoCurrentListObject.Value.Exists(item => item.Index == 1))
                {
                    _OnBadPresentation.RaiseEvent("Sorry I'm not looking for weed.");

                }
                else if (_rsoCurrentListObject.Value.Exists(item => item.Index == 2))
                {
                    _OnGoodPresentation.RaiseEvent("Hey hey! You look like you can buy a lot of sugar!\r\nI'm Kathleen.");

                }
                else if (_rsoCurrentListObject.Value.Exists(item => item.Index == 0) == false && _rsoCurrentListObject.Value.Exists(item => item.Index == 1) == false && _rsoCurrentListObject.Value.Exists(item => item.Index == 2) == false)
                {
                    _OnBadPresentation.RaiseEvent("I'm not here for a baby.\r\nGood bye.");
                }
                break;

            case ProfilType.Babos:
                if (_rsoCurrentListObject.Value.Exists(item => item.Index == 0))
                {
                    _OnGoodPresentation.RaiseEvent("I don't like your style but I'm open minded.\r\nI'm Meloe.");

                }
                else if (_rsoCurrentListObject.Value.Exists(item => item.Index == 1))
                {
                    _OnBadPresentation.RaiseEvent("Hey! Nice to meet you my name is Meloe\r\nI love your outfit, is it made of vegetal fibers?!");

                }
                else if (_rsoCurrentListObject.Value.Exists(item => item.Index == 2))
                {
                    _OnGoodPresentation.RaiseEvent("You look like you would destroy the planet for your own profit.\r\nNope, I'm out of here!");

                }
                else if (_rsoCurrentListObject.Value.Exists(item => item.Index == 0) == false && _rsoCurrentListObject.Value.Exists(item => item.Index == 1) == false && _rsoCurrentListObject.Value.Exists(item => item.Index == 2) == false)
                {
                    _OnBadPresentation.RaiseEvent("Natural, I love it!\r\nI'm Meloe, nice to meet you.");

                }
                break;

            case ProfilType.Rich:
                if (_rsoCurrentListObject.Value.Exists(item => item.Index == 0))
                {
                    _OnBadPresentation.RaiseEvent("I'm not mixing up with scums!\r\nGet out before I call the Police.");

                }
                else if (_rsoCurrentListObject.Value.Exists(item => item.Index == 1))
                {
                    _OnBadPresentation.RaiseEvent("It's smelling bad here? Who let homeless people enter her?\r\nI'm out!");

                }
                else if (_rsoCurrentListObject.Value.Exists(item => item.Index == 2))
                {
                    _OnGoodPresentation.RaiseEvent("Good Evenning, I'm the first son of the Viscount of Salsbury: Baltazar.");

                }
                else if (_rsoCurrentListObject.Value.Exists(item => item.Index == 0) == false && _rsoCurrentListObject.Value.Exists(item => item.Index == 1) == false && _rsoCurrentListObject.Value.Exists(item => item.Index == 2) == false)
                {
                    _OnBadPresentation.RaiseEvent("Dear Lord! Good bye.");

                }
                break;
        }
    }
}