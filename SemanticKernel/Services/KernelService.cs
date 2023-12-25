using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.KernelMemory;
using Microsoft.KernelMemory.Prompts;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Planning;
using Microsoft.SemanticKernel.Planning.Handlebars;
using SemanticKernel.Configs;
using Microsoft.SemanticKernel.Plugins.Core;
using SemanticKernel.Plugins;
using MathPlugin = SemanticKernel.Plugins.MathPlugin;

namespace SemanticKernel.Services;

public class KernelService
{
    public async Task RunPromptKernel()
    {
        // Load the kernel settings
        var kernelSettings = KernelSettings.LoadSettings();

        // Create kernel
        var builder = Kernel.CreateBuilder();
        builder.Services.AddOpenAIChatCompletion(kernelSettings.DeploymentOrModelId, kernelSettings.ApiKey,
            kernelSettings.OrgId, kernelSettings.ServiceId);
        var kernel = builder.Build();

        Console.Write("Your request: ");
        string request = Console.ReadLine()!;

        string prompt = $"What is the intent of this request? {request}";

        var response = await kernel.InvokePromptAsync(prompt);

        Console.WriteLine(response);
    }

    public async Task RunPromptKernel2()
    {
        // Load the kernel settings
        var kernelSettings = KernelSettings.LoadSettings();

        // Create kernel
        var builder = Kernel.CreateBuilder();
        builder.Services.AddOpenAIChatCompletion(kernelSettings.DeploymentOrModelId, kernelSettings.ApiKey,
            kernelSettings.OrgId, kernelSettings.ServiceId);
        var kernel = builder.Build();

        Console.Write("Your request: ");
        string request = Console.ReadLine()!;

        var prompt = $"""
                      What is the intent of this request? {request}
                      You can choose between SendEmail, SendMessage, CompleteTask, CreateDocument.
                      """;

        var response = await kernel.InvokePromptAsync(prompt);

        Console.WriteLine(response);
    }

    public async Task RunPromptKernel3()
    {
        // Load the kernel settings
        var kernelSettings = KernelSettings.LoadSettings();

        // Create kernel
        var builder = Kernel.CreateBuilder();
        builder.Services.AddOpenAIChatCompletion(kernelSettings.DeploymentOrModelId, kernelSettings.ApiKey,
            kernelSettings.OrgId, kernelSettings.ServiceId);
        var kernel = builder.Build();

        Console.Write("Your request: ");
        string request = Console.ReadLine()!;

        var prompt = $"""
                      Instructions: What is the intent of this request?
                      Choices: SendEmail, SendMessage, CompleteTask, CreateDocument.
                      User Input: {request}
                      Intent:
                      """;

        var response = await kernel.InvokePromptAsync(prompt);

        Console.WriteLine(response);
    }

    public async Task RunPromptKernel4()
    {
        // Load the kernel settings
        var kernelSettings = KernelSettings.LoadSettings();

        // Create kernel
        var builder = Kernel.CreateBuilder();
        builder.Services.AddOpenAIChatCompletion(kernelSettings.DeploymentOrModelId, kernelSettings.ApiKey,
            kernelSettings.OrgId, kernelSettings.ServiceId);
        var kernel = builder.Build();

        Console.Write("Your request: ");
        string request = Console.ReadLine()!;

        var prompt = $$"""
                       ## Instructions
                       Provide the intent of the request using the following format:

                       ```json
                       {
                           "intent": {intent}
                       }
                       ```

                       ## Choices
                       You can choose between the following intents:

                       ```json
                       ["SendEmail", "SendMessage", "CompleteTask", "CreateDocument"]
                       ```

                       ## User Input
                       The user input is:

                       ```json
                       {
                           "request": "{{request}}"
                       }
                       ```

                       ## Intent
                       """;

        var response = await kernel.InvokePromptAsync(prompt);

        Console.WriteLine(response);
    }

    public async Task RunPromptKernelWithExample()
    {
        // Load the kernel settings
        var kernelSettings = KernelSettings.LoadSettings();

        // Create kernel
        var builder = Kernel.CreateBuilder();
        builder.Services.AddOpenAIChatCompletion(kernelSettings.DeploymentOrModelId, kernelSettings.ApiKey,
            kernelSettings.OrgId, kernelSettings.ServiceId);
        var kernel = builder.Build();

        Console.Write("Your request: ");
        string request = Console.ReadLine()!;

        var prompt = $"""
                      Instructions: What is the intent of this request?
                      Choices: SendEmail, SendMessage, CompleteTask, CreateDocument.

                      User Input: Can you send a very quick approval to the marketing team?
                      Intent: SendMessage

                      User Input: Can you send the full update to the marketing team?
                      Intent: SendEmail

                      User Input: {request}
                      Intent:
                      """;

        var response = await kernel.InvokePromptAsync(prompt);

        Console.WriteLine(response);
    }

    public async Task RunPromptKernelWithAvoid()
    {
        // Load the kernel settings
        var kernelSettings = KernelSettings.LoadSettings();

        // Create kernel
        var builder = Kernel.CreateBuilder();
        builder.Services.AddOpenAIChatCompletion(kernelSettings.DeploymentOrModelId, kernelSettings.ApiKey,
            kernelSettings.OrgId, kernelSettings.ServiceId);
        var kernel = builder.Build();

        Console.Write("Your request: ");
        string request = Console.ReadLine()!;

        var prompt = $"""
                      Instructions: What is the intent of this request?
                      If you don't know the intent, don't guess; instead respond with "Unknown".
                      Choices: SendEmail, SendMessage, CompleteTask, CreateDocument, Unknown.

                      User Input: Can you send a very quick approval to the marketing team?
                      Intent: SendMessage

                      User Input: Can you send the full update to the marketing team?
                      Intent: SendEmail

                      User Input: {request}
                      Intent:
                      """;

        var response = await kernel.InvokePromptAsync(prompt);

        Console.WriteLine(response);
    }

    public async Task RunPromptKernelWithHistory()
    {
        // Load the kernel settings
        var kernelSettings = KernelSettings.LoadSettings();

        // Create kernel
        var builder = Kernel.CreateBuilder();
        builder.Services.AddOpenAIChatCompletion(kernelSettings.DeploymentOrModelId, kernelSettings.ApiKey,
            kernelSettings.OrgId, kernelSettings.ServiceId);
        var kernel = builder.Build();

        Console.Write("Your request: ");
        string request = Console.ReadLine()!;

        string history = """
                         User input: I hate sending emails, no one ever reads them.
                         AI response: I'm sorry to hear that. Messages may be a better way to communicate.
                         """;

        var prompt = $"""
                      Instructions: What is the intent of this request?
                      If you don't know the intent, don't guess; instead respond with "Unknown".
                      Choices: SendEmail, SendMessage, CompleteTask, CreateDocument, Unknown.

                      User Input: Can you send a very quick approval to the marketing team?
                      Intent: SendMessage

                      User Input: Can you send the full update to the marketing team?
                      Intent: SendEmail

                      {history}
                      User Input: {request}
                      Intent:
                      """;

        var response = await kernel.InvokePromptAsync(prompt);

        Console.WriteLine(response);
    }

    public async Task RunPromptKernelWithVariable()
    {
        // Load the kernel settings
        var kernelSettings = KernelSettings.LoadSettings();

        // Create kernel
        var builder = Kernel.CreateBuilder();
        builder.Services.AddOpenAIChatCompletion(kernelSettings.DeploymentOrModelId, kernelSettings.ApiKey,
            kernelSettings.OrgId, kernelSettings.ServiceId);
        var kernel = builder.Build();

        // Create a Semantic Kernel template for chat
        var chat = kernel.CreateFunctionFromPrompt(
            """
                {{$history}}
                User: {{$request}}
                Assistant:
            """
        );

        ChatHistory history = [];

        // Start the chat loop
        while (true)
        {
            // Get user input
            Console.Write("User > ");
            var request = Console.ReadLine();

            // Get chat response
            var chatResult = kernel.InvokeStreamingAsync<StreamingChatMessageContent>(
                chat,
                new()
                {
                    { "request", request },
                    { "history", string.Join("\n", history.Select(x => x.Role + ": " + x.Content)) }
                }
            );

            // Stream the response
            string message = "";
            await foreach (var chunk in chatResult)
            {
                if (chunk.Role.HasValue) Console.Write(chunk.Role + " > ");
                message += chunk;
                Console.Write(chunk);
            }

            Console.WriteLine();

            // Append to history
            history.AddUserMessage(request!);
            history.AddAssistantMessage(message);
        }
    }

    public async Task RunPromptKernelWithPlugin()
    {
        // Load the kernel settings
        var kernelSettings = KernelSettings.LoadSettings();

        // Create kernel
        var builder = Kernel.CreateBuilder();
        builder.Services.AddOpenAIChatCompletion(kernelSettings.DeploymentOrModelId, kernelSettings.ApiKey,
            kernelSettings.OrgId, kernelSettings.ServiceId);
        var kernel = builder.Build();
        // https://www.developerscantina.com/p/semantic-kernel-native-functions-1-0/
#pragma warning disable SKEXP0050
        kernel.ImportPluginFromType<ConversationSummaryPlugin>();

        // Create a Semantic Kernel template for chat
        var chat = kernel.CreateFunctionFromPrompt(
            """
                {{ConversationSummaryPlugin.SummarizeConversation $history}}
                User: {{$request}}
                Assistant:
            """
        );

        // Create chat history
        ChatHistory history = [];

        // Start the chat loop
        while (true)
        {
            // Get user input
            Console.Write("User > ");
            var request = Console.ReadLine();


            // Get chat response
            var chatResult = kernel.InvokeStreamingAsync<StreamingChatMessageContent>(
                chat,
                new()
                {
                    { "request", request },
                    { "history", string.Join("\n", history.Select(x => x.Role + ": " + x.Content)) }
                }
            );

            // Stream the response
            string message = "";
            await foreach (var chunk in chatResult)
            {
                if (chunk.Role.HasValue) Console.Write(chunk.Role + " > ");
                message += chunk;
                Console.Write(chunk);
            }

            Console.WriteLine();

            // Append to history
            history.AddUserMessage(request!);
            history.AddAssistantMessage(message);
        }
#pragma warning restore SKEXP0042
    }

    public async Task RunPromptKernelWithPromptTemplate()
    {
        // Load the kernel settings
        var kernelSettings = KernelSettings.LoadSettings();

        // Create kernel
        var builder = Kernel.CreateBuilder();
        builder.Services.AddOpenAIChatCompletion(kernelSettings.DeploymentOrModelId, kernelSettings.ApiKey,
            kernelSettings.OrgId, kernelSettings.ServiceId);
        var kernel = builder.Build();
        // https://www.developerscantina.com/p/semantic-kernel-native-functions-1-0/
#pragma warning disable SKEXP0050
        kernel.ImportPluginFromType<ConversationSummaryPlugin>();

        // Create a Semantic Kernel template for chat
        var chat = kernel.CreateFunctionFromPrompt(
            new PromptTemplateConfig()
            {
                Name = "Chat",
                Description = "Chat with the assistant.",
                Template = """
                           {{ConversationSummaryPlugin.SummarizeConversation $history}}
                                   User: {{$request}}
                                   Assistant:
                           """,
                TemplateFormat = "semantic-kernel",
                InputVariables =
                [
                    new()
                    {
                        Name = "history", Description = "The history of the conversation.", IsRequired = false,
                        Default = ""
                    },
                    new() { Name = "request", Description = "The user's request.", IsRequired = true }
                ],
                ExecutionSettings =
                {
                    {
                        "default", new OpenAIPromptExecutionSettings()
                        {
                            MaxTokens = 1000,
                            Temperature = 0
                        }
                    },
                    {
                        "gpt-3.5-turbo", new OpenAIPromptExecutionSettings()
                        {
                            ModelId = "gpt-3.5-turbo-0613",
                            MaxTokens = 4000,
                            Temperature = 0.2
                        }
                    },
                    {
                        "gpt-4", new OpenAIPromptExecutionSettings()
                        {
                            ModelId = "gpt-4-1106-preview",
                            MaxTokens = 8000,
                            Temperature = 0.3
                        }
                    }
                }
            }
        );

        // Create chat history
        ChatHistory history = [];

        // Start the chat loop
        while (true)
        {
            // Get user input
            Console.Write("User > ");
            var request = Console.ReadLine();
            
            // Get chat response
            var chatResult = kernel.InvokeStreamingAsync<StreamingChatMessageContent>(
                chat,
                new()
                {
                    { "request", request },
                    { "history", string.Join("\n", history.Select(x => x.Role + ": " + x.Content)) }
                }
            );

            // Stream the response
            string message = "";
            await foreach (var chunk in chatResult)
            {
                if (chunk.Role.HasValue) Console.Write(chunk.Role + " > ");
                message += chunk;
                Console.Write(chunk);
            }

            Console.WriteLine();

            // Append to history
            history.AddUserMessage(request!);
            history.AddAssistantMessage(message);
        }
#pragma warning restore SKEXP0042
    }

    public async Task RunPromptKernelWithPromptFiles()
    {
        // Load the kernel settings
        var kernelSettings = KernelSettings.LoadSettings();

        // Create kernel
        var builder = Kernel.CreateBuilder();
        builder.Services.AddOpenAIChatCompletion(kernelSettings.DeploymentOrModelId, kernelSettings.ApiKey,
            kernelSettings.OrgId, kernelSettings.ServiceId);
        builder.Services.AddLogging(c => c.AddDebug().SetMinimumLevel(LogLevel.Trace));
        var kernel = builder.Build();
        kernel.ImportPluginFromType<ConversationSummaryPlugin>();
        
        // Load Prompts
        var prompts = kernel.CreatePluginFromPromptDirectory("Prompts");

        // Create chat history
        ChatHistory history = [];

        // Start the chat loop
        while (true)
        {
            // Get user input
            Console.Write("User > ");
            var request = Console.ReadLine();

            // Get chat response
            var chatResult = kernel.InvokeStreamingAsync<StreamingChatMessageContent>(
                prompts["Chat"],
                new() {
                    { "request", request },
                    { "history", string.Join("\n", history.Select(x => x.Role + ": " + x.Content)) }
                }
            );

            // Stream the response
            string message = "";
            await foreach (var chunk in chatResult)
            {
                if (chunk.Role.HasValue) Console.Write(chunk.Role + " > ");
                message += chunk;
                Console.Write(chunk);
            }
            Console.WriteLine();

            // Append to history
            history.AddUserMessage(request!);
            history.AddAssistantMessage(message);
        }
    }

    public async Task RunPromptKernelWithNativePlugin()
    {
        // Load the kernel settings
        var kernelSettings = KernelSettings.LoadSettings();

        // Create kernel
        var builder = Kernel.CreateBuilder();
        builder.Services.AddOpenAIChatCompletion(kernelSettings.DeploymentOrModelId, kernelSettings.ApiKey,
            kernelSettings.OrgId, kernelSettings.ServiceId);
        builder.Services.AddLogging(c => c.AddDebug().SetMinimumLevel(LogLevel.Trace));
        builder.Plugins.AddFromType<AuthorEmailPlanner>();
        builder.Plugins.AddFromType<EmailPlugin>();
        var kernel = builder.Build();
        kernel.ImportPluginFromType<ConversationSummaryPlugin>();
        
        // Retrieve the chat completion service from the kernel
        IChatCompletionService chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

        // Create the chat history
        ChatHistory chatMessages = new ChatHistory("""
                                                   You are a friendly assistant who likes to follow the rules. You will complete required steps
                                                   and request approval before taking any consequential actions. If the user doesn't provide
                                                   enough information for you to complete a task, you will keep asking questions until you have
                                                   enough information to complete the task.
                                                   """);

        // Start the conversation
        while (true)
        {
            // Get user input
            System.Console.Write("User > ");
            chatMessages.AddUserMessage(Console.ReadLine()!);

            // Get the chat completions
            OpenAIPromptExecutionSettings openAiPromptExecutionSettings = new()
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };
            var result = chatCompletionService.GetStreamingChatMessageContentsAsync(
                chatMessages,
                executionSettings: openAiPromptExecutionSettings,
                kernel: kernel);

            // Stream the results
            string fullMessage = "";
            await foreach (var content in result)
            {
                if (content.Role.HasValue)
                {
                    Console.Write("Assistant > ");
                }
                Console.Write(content.Content);
                fullMessage += content.Content;
            }
            Console.WriteLine();

            // Add the message from the agent to the chat history
            chatMessages.AddAssistantMessage(fullMessage);
        }
    }
    
    public async Task RunKernelMathPlugin()
    {
        // Load the kernel settings
        var kernelSettings = KernelSettings.LoadSettings();

        // Create kernel
        var builder = Kernel.CreateBuilder();
        builder.Services.AddOpenAIChatCompletion(kernelSettings.DeploymentOrModelId, kernelSettings.ApiKey,
            kernelSettings.OrgId, kernelSettings.ServiceId);
        builder.Services.AddLogging(c => c.AddDebug().SetMinimumLevel(LogLevel.Trace));
        builder.Plugins.AddFromType<MathPlugin>();
        var kernel = builder.Build();
        kernel.ImportPluginFromType<ConversationSummaryPlugin>();
        
        // Create chat history
        ChatHistory history = [];

        // Get chat completion service
        var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

        // Test the math plugin
        double answer = await kernel.InvokeAsync<double>(
            "MathPlugin", "Sqrt",
            new() {
                { "number1", 12 }
            }
        );
        Console.WriteLine($"The square root of 12 is {answer}.");

        // Start the conversation
        while (true)
        {
            // Get user input
            Console.Write("User > ");
            history.AddUserMessage(Console.ReadLine()!);

            // Enable auto function calling
            OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            // Get the response from the AI
            var result = chatCompletionService.GetStreamingChatMessageContentsAsync(
                history,
                executionSettings: openAIPromptExecutionSettings,
                kernel: kernel);

            // Stream the results
            string fullMessage = "";
            var first = true;
            await foreach (var content in result)
            {
                if (content.Role.HasValue && first)
                {
                    Console.Write("Assistant > ");
                    first = false;
                }
                Console.Write(content.Content);
                fullMessage += content.Content;
            }
            Console.WriteLine();

            // Add the message from the agent to the chat history
            history.AddAssistantMessage(fullMessage);
        }
    }
    
    public async Task RunKernelWithHandlebarsPlanner()
    {
        // Load the kernel settings
        var kernelSettings = KernelSettings.LoadSettings();

        // Create kernel
        var builder = Kernel.CreateBuilder();
        builder.Services.AddOpenAIChatCompletion(kernelSettings.DeploymentOrModelId, kernelSettings.ApiKey,
            kernelSettings.OrgId, kernelSettings.ServiceId);
        builder.Services.AddLogging(c => c.AddDebug().SetMinimumLevel(LogLevel.Trace));
        builder.Plugins.AddFromType<MathPlugin>();
        var kernel = builder.Build();
        kernel.ImportPluginFromType<ConversationSummaryPlugin>();
#pragma warning disable SKEXP0060
        var planner = new HandlebarsPlanner(
            new HandlebarsPlannerOptions()
            {
                AllowLoops = false
            });

        var goal = "2*2 nin degerini hesapla ve bu degerin karesini al";
        
        Console.WriteLine($"Goal: {goal}");

        // Create the plan
        var plan = await planner.CreatePlanAsync(kernel, goal);
        
        // Print the prompt template
        if (plan.Prompt is not null)
        {
            Console.WriteLine($"\nPrompt template:\n{plan.Prompt}");
        }

        Console.WriteLine($"\nOriginal plan:\n{plan}");

        // Execute the plan
        var result = await plan.InvokeAsync(kernel);
        Console.WriteLine($"\nResult:\n{result}\n");
#pragma warning restore SKEXP0060
    }
    
    public async Task RunKernelWithStepwiseFunctionPlanner()
    {
        // Load the kernel settings
        var kernelSettings = KernelSettings.LoadSettings();

        // Create kernel
        var builder = Kernel.CreateBuilder();
        builder.Services.AddOpenAIChatCompletion(kernelSettings.DeploymentOrModelId, kernelSettings.ApiKey,
            kernelSettings.OrgId, kernelSettings.ServiceId);
        builder.Services.AddLogging(c => c.AddDebug().SetMinimumLevel(LogLevel.Debug));
        builder.Plugins.AddFromType<MathPlugin>();
        builder.Plugins.AddFromType<TimePlugin>();
        var kernel = builder.Build();
        kernel.ImportPluginFromType<ConversationSummaryPlugin>();
#pragma warning disable SKEXP0060
        var config = new FunctionCallingStepwisePlannerConfig
        {
            MaxIterations = 15,
            MaxTokens = 4000,
        };
        var planner = new FunctionCallingStepwisePlanner(config);

        var goal = "What is the current hour number, plus 5?";
        
        Console.WriteLine($"Goal: {goal}");

        FunctionCallingStepwisePlannerResult result = await planner.ExecuteAsync(kernel, goal);
        Console.WriteLine($"Q: {goal}\nA: {result.FinalAnswer}");
#pragma warning restore SKEXP0060
    }
    
    public async Task RunKernelWithMemory()
    {
        // Load the kernel settings
        var kernelSettings = KernelSettings.LoadSettings();

        OpenAIConfig openAiConfig = new OpenAIConfig();
        openAiConfig.TextModel = "gpt-3.5-turbo-16k";
        openAiConfig.TextModelMaxTokenTotal = 16384;
        openAiConfig.EmbeddingModel = "text-embedding-ada-002";
        openAiConfig.EmbeddingModelMaxTokenTotal = 8191;
        openAiConfig.APIKey = kernelSettings.ApiKey;
        openAiConfig.OrgId = kernelSettings.OrgId;
        openAiConfig.MaxRetries = 10;
        
        var memory = new KernelMemoryBuilder()
            .WithCustomPromptProvider(new MyPromptProvider())
            .WithOpenAI(openAiConfig)
            .Build<MemoryServerless>();
        
        await memory.ImportTextAsync("NASA space probe Lucy flies by asteroid 152830 Dinkinesh, the first of eight asteroids planned to be visited by the spacecraft.");
        
        var statement = "Lucy flied by an asteroid";
        var verification = await memory.AskAsync(statement);
        Console.WriteLine($"{statement} => {verification.Result}");

        statement = "Lucy landed on an asteroid";
        verification = await memory.AskAsync(statement);
        Console.WriteLine($"{statement} => {verification.Result}");

        statement = "Lucy is powered by a nuclear engine";
        verification = await memory.AskAsync(statement);
        Console.WriteLine($"{statement} => {verification.Result}");
    }
    
    public class MyPromptProvider : IPromptProvider
    {
        private const string VerificationPrompt = """
                                                  Facts:
                                                  {{$facts}}
                                                  ======
                                                  Given only the facts above, verify the fact below.
                                                  You don't know where the knowledge comes from, just answer.
                                                  If you have sufficient information to verify, reply only with 'TRUE', nothing else.
                                                  If you have sufficient information to deny, reply only with 'FALSE', nothing else.
                                                  If you don't have sufficient information, reply with 'NEED MORE INFO'.
                                                  User: {{$input}}
                                                  Verification:
                                                  """;

        private readonly EmbeddedPromptProvider _fallbackProvider = new();

        public string ReadPrompt(string promptName)
        {
            switch (promptName)
            {
                case Constants.PromptNamesAnswerWithFacts:
                    return VerificationPrompt;

                default:
                    // Fall back to the default
                    return this._fallbackProvider.ReadPrompt(promptName);
            }
        }
    }
}