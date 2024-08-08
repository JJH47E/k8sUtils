using K8sUtils.Models.GetPodsResponse;
using Newtonsoft.Json;

namespace K8sUtils.ProcessHosts;

public class StubKubectlHost : IKubectlHost
{
    public async Task<GetPodsResponse> ListPods(string @namespace)
    {
        await Task.Delay(2000);
        
        #region json
        var json = JsonConvert.DeserializeObject<GetPodsResponse>(@"{
    ""apiVersion"": ""v1"",
    ""items"": [
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""creationTimestamp"": ""2024-08-02T18:21:55Z"",
                ""generateName"": ""hello-minikube-5c898d8489-"",
                ""labels"": {
                    ""app"": ""hello-minikube"",
                    ""pod-template-hash"": ""5c898d8489""
                },
                ""name"": ""hello-minikube-5c898d8489-9vnmq"",
                ""namespace"": ""default"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""apps/v1"",
                        ""blockOwnerDeletion"": true,
                        ""controller"": true,
                        ""kind"": ""ReplicaSet"",
                        ""name"": ""hello-minikube-5c898d8489"",
                        ""uid"": ""34326595-70b1-4abd-88c7-df038c78b3b5""
                    }
                ],
                ""resourceVersion"": ""9037"",
                ""uid"": ""75f6b256-20c6-4d70-8db0-33ee5e17e367""
            },
            ""spec"": {
                ""containers"": [
                    {
                        ""image"": ""kicbase/echo-server:1.0"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""name"": ""echo-server"",
                        ""resources"": {},
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/var/run/secrets/kubernetes.io/serviceaccount"",
                                ""name"": ""kube-api-access-g75rk"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""ClusterFirst"",
                ""enableServiceLinks"": true,
                ""nodeName"": ""minikube"",
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 0,
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""serviceAccount"": ""default"",
                ""serviceAccountName"": ""default"",
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/not-ready"",
                        ""operator"": ""Exists"",
                        ""tolerationSeconds"": 300
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/unreachable"",
                        ""operator"": ""Exists"",
                        ""tolerationSeconds"": 300
                    }
                ],
                ""volumes"": [
                    {
                        ""name"": ""kube-api-access-g75rk"",
                        ""projected"": {
                            ""defaultMode"": 420,
                            ""sources"": [
                                {
                                    ""serviceAccountToken"": {
                                        ""expirationSeconds"": 3607,
                                        ""path"": ""token""
                                    }
                                },
                                {
                                    ""configMap"": {
                                        ""items"": [
                                            {
                                                ""key"": ""ca.crt"",
                                                ""path"": ""ca.crt""
                                            }
                                        ],
                                        ""name"": ""kube-root-ca.crt""
                                    }
                                },
                                {
                                    ""downwardAPI"": {
                                        ""items"": [
                                            {
                                                ""fieldRef"": {
                                                    ""apiVersion"": ""v1"",
                                                    ""fieldPath"": ""metadata.namespace""
                                                },
                                                ""path"": ""namespace""
                                            }
                                        ]
                                    }
                                }
                            ]
                        }
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2024-08-04T12:25:18Z"",
                        ""status"": ""True"",
                        ""type"": ""PodReadyToStartContainers""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2024-08-02T18:21:55Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2024-08-04T12:25:18Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2024-08-04T12:25:18Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2024-08-02T18:21:55Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""docker://588ba0d3b830ca062f01d35038a4844965bf1ad55955d5a805693970461071d6"",
                        ""image"": ""kicbase/echo-server:1.0"",
                        ""imageID"": ""docker-pullable://kicbase/echo-server@sha256:127ac38a2bb9537b7f252addff209ea6801edcac8a92c8b1104dacd66a583ed6"",
                        ""lastState"": {
                            ""terminated"": {
                                ""containerID"": ""docker://7b48d26a87715443bed1813d86807b3b448ec65e75a830b08c39ab29f2277d1c"",
                                ""exitCode"": 2,
                                ""finishedAt"": ""2024-08-03T19:53:22Z"",
                                ""reason"": ""Error"",
                                ""startedAt"": ""2024-08-03T19:36:28Z""
                            }
                        },
                        ""name"": ""echo-server"",
                        ""ready"": true,
                        ""restartCount"": 3,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2024-08-04T12:25:18Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""192.168.49.2"",
                ""hostIPs"": [
                    {
                        ""ip"": ""192.168.49.2""
                    }
                ],
                ""phase"": ""Running"",
                ""podIP"": ""10.244.0.15"",
                ""podIPs"": [
                    {
                        ""ip"": ""10.244.0.15""
                    }
                ],
                ""qosClass"": ""BestEffort"",
                ""startTime"": ""2024-08-02T18:21:55Z""
            }
        }
    ],
    ""kind"": ""List"",
    ""metadata"": {
        ""resourceVersion"": """"
    }
}");
        return json!;
        #endregion
    }

    public Task<IEnumerable<string>> GetLogs(string podName, string @namespace)
    {
        var logs = Enumerable.Range(1, 10).Select(i => $"{podName} log {i}").ToList();

        return Task.FromResult(logs.AsEnumerable());
    }
}