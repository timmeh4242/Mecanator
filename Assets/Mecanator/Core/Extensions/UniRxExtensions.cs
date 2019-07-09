using System; // require keep for Windows Universal App
using UnityEngine;
using System.Linq;
using UniRx;

namespace UniRx.Triggers
{
    public static partial class UniRxExtensions
    {
        public static IObservable<ObservableStateMachineTrigger.OnStateInfo> OnStateEnter(this Animator animator, string fullPath)
        {
            int fullPathHash = Animator.StringToHash(fullPath);
            return animator.OnStateEnter(fullPathHash);
        }

        public static IObservable<ObservableStateMachineTrigger.OnStateInfo> OnStateEnter(this Animator animator, int fullPathHash)
        {
            var observableStateMachineTriggers = animator.GetBehaviours<ObservableStateMachineTrigger>();
            if (observableStateMachineTriggers.Length <= 0 || observableStateMachineTriggers == null)
            {
                Debug.LogWarning("No ObservableStateMachineTriggers were found on " + animator.name + " animator!");
                return null;
            }

            var observableTriggers = observableStateMachineTriggers.Select(trigger => trigger.OnStateEnterAsObservable()
                .Where(onStateInfo => onStateInfo.StateInfo.fullPathHash == fullPathHash));

            return Observable.Merge(observableTriggers);
        }

        public static IObservable<ObservableStateMachineTrigger.OnStateInfo> OnStateUpdate(this Animator animator, params string[] fullPaths)
        {
            var fullPathHashes = fullPaths.Select(s => Animator.StringToHash(s)).ToArray();
            return animator.OnStateUpdate(fullPathHashes);
        }

        public static IObservable<ObservableStateMachineTrigger.OnStateInfo> OnStateUpdate(this Animator animator, params int[] fullPathHashes)
        {
            var observableStateMachineTriggers = animator.GetBehaviours<ObservableStateMachineTrigger>();
            if (observableStateMachineTriggers.Length <= 0 || observableStateMachineTriggers == null)
            {
                Debug.LogWarning("No ObservableStateMachineTriggers were found on " + animator.name + " animator!");
                return null;
            }

            var observableTriggers = observableStateMachineTriggers.Select(trigger => trigger.OnStateUpdateAsObservable()
                .Where(onStateInfo => fullPathHashes.Contains(onStateInfo.StateInfo.fullPathHash)));

            return Observable.Merge(observableTriggers);
        }

        public static IObservable<ObservableStateMachineTrigger.OnStateInfo> OnStateNormalizedTime(this Animator animator, string fullPath, float normalizedTime)
        {
            var observableStateMachineTriggers = animator.GetBehaviours<ObservableStateMachineTrigger>();
            if (observableStateMachineTriggers.Length <= 0 || observableStateMachineTriggers == null)
            {
                Debug.LogWarning("No ObservableStateMachineTriggers were found on " + animator.name + " animator!");
                return null;
            }

            int fullPathHash = Animator.StringToHash(fullPath);

            var emit = false;

            var observableTriggers = observableStateMachineTriggers.Select(trigger => trigger.OnStateUpdateAsObservable()
                .Where(onStateInfo =>
                {
                    if (onStateInfo.StateInfo.fullPathHash == fullPathHash)
                    {
                        if (emit)
                        {
                            if (onStateInfo.StateInfo.normalizedTime >= normalizedTime)
                            {
                                emit = false;
                                return true;
                            }
                        }
                        else
                        {
                            if (onStateInfo.StateInfo.normalizedTime < normalizedTime)
                            {
                                emit = true;
                            }
                        }

                    }

                    return false;
                }));

            return Observable.Merge(observableTriggers);
        }

        public static IObservable<ObservableStateMachineTrigger.OnStateInfo> OnStateTime(this Animator animator, string fullPath, float time)
        {
            var observableStateMachineTriggers = animator.GetBehaviours<ObservableStateMachineTrigger>();
            if (observableStateMachineTriggers.Length <= 0 || observableStateMachineTriggers == null)
            {
                Debug.LogWarning("No ObservableStateMachineTriggers were found on " + animator.name + " animator!");
                return null;
            }

            int fullPathHash = Animator.StringToHash(fullPath);

            var emit = false;

            var observableTriggers = observableStateMachineTriggers.Select(trigger => trigger.OnStateUpdateAsObservable()
                .Where(onStateInfo =>
                {
                    if (onStateInfo.StateInfo.fullPathHash == fullPathHash)
                    {
                        if (emit)
                        {
                            if (onStateInfo.StateInfo.normalizedTime * onStateInfo.StateInfo.length >= time)
                            {
                                emit = false;
                                return true;
                            }
                        }
                        else
                        {
                            if (onStateInfo.StateInfo.normalizedTime * onStateInfo.StateInfo.length < time)
                            {
                                emit = true;
                            }
                        }

                    }

                    return false;
                }));

            return Observable.Merge(observableTriggers);
        }

        public static IObservable<ObservableStateMachineTrigger.OnStateInfo> OnStateExit(this Animator animator, string fullPath)
        {
            int fullPathHash = Animator.StringToHash(fullPath);
            return animator.OnStateExit(fullPathHash);
        }

        public static IObservable<ObservableStateMachineTrigger.OnStateInfo> OnStateExit(this Animator animator, int fullPathHash)
        {
            var observableStateMachineTriggers = animator.GetBehaviours<ObservableStateMachineTrigger>();
            if (observableStateMachineTriggers.Length <= 0 || observableStateMachineTriggers == null)
            {
                Debug.LogWarning("No ObservableStateMachineTriggers were found on " + animator.name + " animator!");
                return null;
            }

            var observableTriggers = observableStateMachineTriggers.Select(trigger => trigger.OnStateExitAsObservable()
                .Where(onStateInfo => onStateInfo.StateInfo.fullPathHash == fullPathHash));

            return Observable.Merge(observableTriggers);
        }
    }
}
