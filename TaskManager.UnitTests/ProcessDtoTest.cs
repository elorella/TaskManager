using TaskManager.Entity;
using Xunit;

namespace TaskManager.UnitTests
{
    public class ProcessDtoTest
    {
        [Theory(DisplayName = "ProcessDto ToString should return a message.")]
        [InlineData(Priority.Low)]
        [InlineData(Priority.Medium)]
        [InlineData(Priority.High)]
        public void ProcessDtoToString(Priority priority)
        {
            var process = new ProcessDto(priority);
            var message = process.ToString();
            Assert.Equal($"PID:{process.PID} Priority:{process.Priority} Create Date:{process.CreateDate}", message);
        }

    }
}