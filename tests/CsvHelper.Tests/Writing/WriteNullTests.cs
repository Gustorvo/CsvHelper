﻿using CsvHelper.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Xunit;

namespace CsvHelper.Tests.Writing
{
	public class WriteNullTests
	{
		[Fact]
		public void WriteRecordsEnumerableGeneric_RecordIsNull_WritesEmptyRecord()
		{
			var records = new List<Foo>
			{
				new Foo { Id = 1, Name = "one"},
				null,
				new Foo { Id = 2, Name = "two" },
			};
			var config = new CsvConfiguration(CultureInfo.InvariantCulture);
			using (var writer = new StringWriter())
			using (var csv = new CsvWriter(writer, config))
			{
				csv.WriteRecords(records);
				csv.Flush();

				var expected = new TestStringBuilder(config.NewLine);
				expected.AppendLine("Id,Name");
				expected.AppendLine("1,one");
				expected.AppendLine(",");
				expected.AppendLine("2,two");

				Assert.Equal(expected, writer.ToString());
			}
		}

		[Fact]
		public void WriteRecordsEnumerable_RecordIsNull_WritesEmptyRecord()
		{
			IEnumerable records = new List<Foo>
			{
				new Foo { Id = 1, Name = "one"},
				null,
				new Foo { Id = 2, Name = "two" },
			};
			var config = new CsvConfiguration(CultureInfo.InvariantCulture);
			using (var writer = new StringWriter())
			using (var csv = new CsvWriter(writer, config))
			{
				csv.WriteRecords(records);
				csv.Flush();

				var expected = new TestStringBuilder(config.NewLine);
				expected.AppendLine("Id,Name");
				expected.AppendLine("1,one");
				expected.AppendLine("");
				expected.AppendLine("2,two");

				Assert.Equal(expected, writer.ToString());
			}
		}

		[Fact]
		public void WriteRecord_RecordIsNull_WritesEmptyRecord()
		{
			var config = new CsvConfiguration(CultureInfo.InvariantCulture);
			using (var writer = new StringWriter())
			using (var csv = new CsvWriter(writer, config))
			{
				csv.WriteRecord((Foo)null);
				csv.Flush();

				Assert.Equal(",", writer.ToString());
			}
		}

		private class Foo
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}
	}
}
