using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using GoogleMobileAds.Ump.Api;

public class Loader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

       // ConsentInformation.Reset();

    //    var debugSettings = new ConsentDebugSettings
    //     {
    //         DebugGeography = DebugGeography.EEA,
    //         TestDeviceHashedIds =
    //         new List<string>
    //         {
    //             "671190401D525E1C97694A5B7A5E4860"
    //         }
    //     }; 
        
        ConsentRequestParameters request = new ConsentRequestParameters();

        // Check the current consent information status.
        ConsentInformation.Update(request, OnConsentInfoUpdated);
        
       
    }

    void OnConsentInfoUpdated(FormError consentError)
{
    if (consentError != null)
    {
        // Handle the error.
        UnityEngine.Debug.LogError(consentError);
        return;
    }
    

    // If the error is null, the consent information state was updated.
    // You are now ready to check if a form is available.
    ConsentForm.LoadAndShowConsentFormIfRequired((FormError formError) =>
    {
        if (formError != null)
        {
            // Consent gathering failed.
            UnityEngine.Debug.LogError(consentError);
            return;
        }

        // Consent has been gathered.
        if (ConsentInformation.CanRequestAds())
        {
            MobileAds.Initialize((InitializationStatus initstatus) =>
            {
            });
            StartCoroutine(LoadGame());
        }
    });
}


    
    IEnumerator LoadGame(){
        AsyncOperation ao = SceneManager.LoadSceneAsync(1);
        ao.allowSceneActivation = false;

        while(ao.isDone == false){

            if(ao.progress == 0.9f){
                Debug.Log("true");

                yield return new WaitForSeconds(2f);

                ao.allowSceneActivation = true;
                
            }

            yield return null;
        }

    }

    
}
