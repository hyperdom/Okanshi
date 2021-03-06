using System.Linq;
using FluentAssertions;
using Xunit;

namespace Okanshi.Test
{
    public class GaugeTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(167)]
        public void Value_is_gotten_through_passed_in_func(int expectedValue)
        {
            var gauge = new Gauge<int>(MonitorConfig.Build("Test"), () => expectedValue);

            gauge.GetValues().First().Value.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(167)]
        public void Value_is_gotten_through_passed_in_func_when_using_get_and_reset(int expectedValue)
        {
            var gauge = new Gauge<int>(MonitorConfig.Build("Test"), () => expectedValue);

            gauge.GetValuesAndReset().First().Value.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(167)]
        public void After_get_and_reset_the_value_is_still_gotten_from_func(int expectedValue)
        {
            var gauge = new Gauge<int>(MonitorConfig.Build("Test"), () => expectedValue);
            gauge.GetValuesAndReset();

            gauge.GetValuesAndReset().First().Value.Should().Be(expectedValue);
        }

        [Fact]
        public void Value_is_called_value()
        {
            var gauge = new Gauge<int>(MonitorConfig.Build("Test"), () => 1);
            gauge.GetValues().Single().Name.Should().Be("value");
        }
    }
}