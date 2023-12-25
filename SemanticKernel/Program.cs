using SemanticKernel.Services;

abstract class Program
{
    private static async Task Main(string[] args)
    {
        var service = new KernelService();
        // await service.RunPromptKernel(); // I want to send an email to the marketing team celebrating their recent milestone.
        // await service.RunPromptKernel2();
        // await service.RunPromptKernel3();
        // await service.RunPromptKernel4();
        // await service.RunPromptKernelWithExample();
        // await service.RunPromptKernelWithAvoid(); // I hate sending emails, no one ever reads them.
        // await service.RunPromptKernelWithHistory(); 
        // await service.RunPromptKernelWithVariable(); 
        // await service.RunPromptKernelWithPlugin(); 
        // await service.RunPromptKernelWithPromptTemplate(); 
        // await service.RunPromptKernelWithPromptFiles();
        // await service.RunPromptKernelWithNativePlugin(); // Can you help me write an email for my boss?
        // await service.RunKernelMathPlugin();
        // await service.RunKernelWithHandlebarsPlanner();
        // await service.RunKernelWithStepwiseFunctionPlanner();
        await service.RunKernelWithMemory();
    }
}
