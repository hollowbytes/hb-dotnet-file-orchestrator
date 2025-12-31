namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IConnectorStrategy
{
    Task SaveAsync(IConnectorOptions options, CancellationToken cancellationToken = default);
}

public interface IConnectorStrategy<in TOptions> : IConnectorStrategy where TOptions : class, IConnectorOptions
{
    Task IConnectorStrategy.SaveAsync(IConnectorOptions options, CancellationToken cancellationToken)
    {
        return SaveAsync((TOptions)options, cancellationToken);
    }

    Task SaveAsync(TOptions options, CancellationToken cancellationToken = default);
}