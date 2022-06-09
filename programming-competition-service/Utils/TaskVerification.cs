using System;
namespace ProgrammingCompetitionService.Utils
{
	public static class TaskVerification
	{
		public static bool IsCompleded(string jdoodleOut, string taskOut)
        {
			var jdoodleOutArr = jdoodleOut.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var taskOutArr = taskOut.Split(';', StringSplitOptions.RemoveEmptyEntries);

			if (jdoodleOutArr.Length != taskOutArr.Length)
            {
				return false;
            }

            for (int i = 0; i < jdoodleOutArr.Length; i++)
            {
                if (jdoodleOutArr[i].Trim().ToLower() != taskOutArr[i].Trim().ToLower())
                {
                    return false;
                }
            }
            return true;
		}
	}
}

