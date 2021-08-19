using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HGuidePostEvent : MonoBehaviour, IPointerClickHandler
{
        //监听点击
    public void OnPointerClick(PointerEventData eventData)
    {
        //事件穿透
      PassEvent(eventData, ExecuteEvents.pointerClickHandler);
    }
    private void PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function) where T : IEventSystemHandler
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);
        var current = data.pointerCurrentRaycast.gameObject;
        for (int i = 0; i < results.Count; i++)
        {
            //判断穿透对象是否是需要要点击的对象
            if (current != results[i].gameObject)
            {
                //// Debug.Log(results[i].gameObject.name);
                //var guidStep = results[i].gameObject.GetComponent<BaseGuideStep>();
                //if (guidStep != null && GuideModel.inst.IsCurrentGuide(guidStep.guideStepId))
                //{
                //    // Debug.Log(current.name);
                //    ExecuteEvents.Execute(results[i].gameObject, data, function);
                //}

            }
        }
    }
}
