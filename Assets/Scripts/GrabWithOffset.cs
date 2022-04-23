using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class GrabWithOffset : XRGrabInteractable
{
    private Vector3 interactorPosition = Vector3.zero;
    private Quaternion interactorRotation = Quaternion.identity;

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
        StoreInteractor(interactor);
        MatchAttachmentPoints(interactor);
    }  

    private void StoreInteractor(XRBaseInteractor interactor)
    {
        // TODO: try to have a slight grab move to the hand so that the user
        //       gets a visual feedback that he grabed something
        interactorPosition = interactor.attachTransform.localPosition;
        // interactorRotation = interactor.attachTransform.localRotation;
    }

    private void MatchAttachmentPoints(XRBaseInteractor interactor)
    {
        bool hasAttached = attachTransform != null;
        interactor.attachTransform.position = hasAttached ? attachTransform.position : transform.position; 
        //interactor.attachTransform.rotation = hasAttached ? attachTransform.rotation : transform.rotation;
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        ResetAttachmentPoints(interactor);
        ClearInteractor(interactor);
    }
    
    private void ResetAttachmentPoints(XRBaseInteractor interactor) 
    {
        interactor.attachTransform.localPosition = interactorPosition;
        //interactor.attachTransform.localRotation = interactorRotation;
    }

    private void ClearInteractor(XRBaseInteractor interactor)
    {
        interactorPosition = Vector3.zero;
        //interactorRotation = Quaternion.identity;
    }
}
