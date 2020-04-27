using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The event manager
/// </summary>
public static class EventManager {

    #region Fields
    static List<Stamina> beaverAnimationTiredInvokers = new List<Stamina>();
    static List<UnityAction<int>> beaverAnimationTiredListeners = new List<UnityAction<int>>();
    #endregion

    #region Methods
    /// <summary>
    /// Adds the parameter as a BeaverAnimationTired event invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddBeaverAnimationTiredInvoker(Stamina invoker) {
        beaverAnimationTiredInvokers.Add(invoker);
        foreach (UnityAction<int> listener in beaverAnimationTiredListeners) {
            invoker.AddBeaverAnimationTiredListener(listener);
        }
    }

    /// <summary>
    /// Adds the parameter as a BeaverAnimationTired event listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddBeaverAnimationTiredListener(UnityAction<int> listener) {
        beaverAnimationTiredListeners.Add(listener);
        foreach (Stamina invoker in beaverAnimationTiredInvokers) {
            invoker.AddBeaverAnimationTiredListener(listener);
        }
    }
    #endregion
}