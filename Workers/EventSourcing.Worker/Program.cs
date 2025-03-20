using EventSourcing.Worker.Consumer;
using MassTransit;
using Serilog;
using SharedKernel.Constants;
using EventSourcing.Application;
using EventSourcing.Infrastructure;

const string DEAD_EXCHANGE = "dead_eventsourcing_exchange";

//BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var builder = Host.CreateApplicationBuilder(args);

var configuration = builder.Configuration;

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.AddSerilog(logger);
logger.Information("Environment: {environment}", builder.Environment.EnvironmentName.ToLower());


try
{
    //MONGO DB CONFIGURATION
    var mongoDbUri = configuration.GetSection("Mongodb:Uri").Value ?? throw new Exception("Mongodb:Uri was not supplied");
    var mongDb = configuration.GetSection("Mongodb:DataBase").Value ?? throw new Exception("Mongodb:DataBase was not supplied");

    builder.Services.AddEventSourcingApplication()
                    .AddEventSourcing(mongoDbUri, mongDb);
                    
    //RABBIT_MQ_CONFIGURATION
    var rabbitHost = configuration.GetSection("RabbitMqSettings:Uri").Value ?? Environment.GetEnvironmentVariable("RABBIT_HOST") ??
throw new Exception("The rabbitMQ connection was not supled");

    builder.Services.AddMassTransit(x =>
    {
        x.AddConsumer<PersonEventSourcingConsumer>();
        x.AddConsumer<ProductEventSourcingConsumer>();
        x.AddConsumer<UsersEventSourcingConsumer>();

        x.UsingRabbitMq((context, config) =>
        {
            config.Host(rabbitHost);
            config.ReceiveEndpoint(Queues.PERSON_EVENTSOURCING_QUEUE, e =>
            {
                e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5))); // Reintenta 3 veces con 5s entre intentos
                e.UseInMemoryOutbox(context);
                e.Consumer<PersonEventSourcingConsumer>(context);
                e.BindDeadLetterQueue(DEAD_EXCHANGE);
            });

            config.ReceiveEndpoint(Queues.USERS_EVENTSOURCING_QUEUE, e =>
            {
                e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5))); // Reintenta 3 veces con 5s entre intentos
                e.UseInMemoryOutbox(context);
                e.Consumer<UsersEventSourcingConsumer>(context);
                e.BindDeadLetterQueue(DEAD_EXCHANGE);
            });

            config.ReceiveEndpoint(Queues.PRODUCT_EVENTSOURCING_QUEUE, e =>
            {
                e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5))); // Reintenta 3 veces con 5s entre intentos
                e.UseInMemoryOutbox(context);
                e.Consumer<ProductEventSourcingConsumer>(context);
                e.BindDeadLetterQueue(DEAD_EXCHANGE);
            });

        });
    });
}
catch (System.Exception)
{

    throw;
}

//builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();