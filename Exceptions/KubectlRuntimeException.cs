namespace K8sUtils.Exceptions;

public class KubectlRuntimeException(string message) : Exception(message);