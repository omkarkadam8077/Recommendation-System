
using CareerRecommendationSystem.Models;

namespace CareerRecommendationSystem.Services
{
    public class CareerRecommendationService
    {
        public List<CareerRecommendation> Recommend(
            CareerAssessment student)
        {
            var careers = new List<CareerRecommendation>
            {
                CreateCareer(
                    "Software Developer",
                    student,
                    student.ProgrammingSkill * 0.55 +
                    student.MathSkill * 0.15 +
                    student.CommunicationSkill * 0.10 +
                    student.Percentage * 0.20,
                    "Develops software applications and websites.",
                    "C#, Java, Python, SQL"
                ),

                CreateCareer(
                    "Data Analyst",
                    student,
                    student.MathSkill * 0.45 +
                    student.ProgrammingSkill * 0.25 +
                    student.Percentage * 0.20 +
                    student.CommunicationSkill * 0.10,
                    "Analyzes data to discover useful information.",
                    "Python, Excel, SQL, Statistics"
                ),

                CreateCareer(
                    "Cybersecurity Analyst",
                    student,
                    student.ProgrammingSkill * 0.35 +
                    student.MathSkill * 0.30 +
                    student.CommunicationSkill * 0.15 +
                    student.Percentage * 0.20,
                    "Protects computer systems and networks.",
                    "Networking, Linux, Security, Python"
                ),

                CreateCareer(
                    "UI/UX Designer",
                    student,
                    student.CommunicationSkill * 0.35 +
                    student.ProgrammingSkill * 0.10 +
                    student.MathSkill * 0.10 +
                    student.Percentage * 0.15 +
                    30,
                    "Designs attractive and user-friendly applications.",
                    "Figma, HTML, CSS, Design"
                ),

                CreateCareer(
                    "Business Analyst",
                    student,
                    student.CommunicationSkill * 0.40 +
                    student.MathSkill * 0.20 +
                    student.ProgrammingSkill * 0.15 +
                    student.Percentage * 0.15 +
                    10,
                    "Analyzes business requirements and processes.",
                    "Excel, SQL, Communication, Analysis"
                ),

                CreateCareer(
                    "Teacher / Lecturer",
                    student,
                    student.CommunicationSkill * 0.50 +
                    student.Percentage * 0.25 +
                    student.MathSkill * 0.10 +
                    student.ProgrammingSkill * 0.05 +
                    10,
                    "Teaches students and shares knowledge.",
                    "Communication, Subject Knowledge, Teaching"
                )
            };

            foreach (var career in careers)
            {
                double bonus = 0;

                string interest = student.Interest.ToLower();

                if (career.CareerName == "Software Developer" &&
                    interest == "technology")
                    bonus = 12;

                else if (career.CareerName == "Data Analyst" &&
                    (interest == "data science" || interest == "science"))
                    bonus = 15;

                else if (career.CareerName == "Cybersecurity Analyst" &&
                    interest == "technology")
                    bonus = 10;

                else if (career.CareerName == "UI/UX Designer" &&
                    interest == "design")
                    bonus = 20;

                else if (career.CareerName == "Business Analyst" &&
                    (interest == "business" || interest == "finance"))
                    bonus = 15;

                else if (career.CareerName == "Teacher / Lecturer" &&
                    interest == "education")
                    bonus = 20;

                if (student.WorkPreference == "Remote" &&
                    (career.CareerName == "Software Developer" ||
                     career.CareerName == "UI/UX Designer" ||
                     career.CareerName == "Data Analyst"))
                    bonus += 3;

                career.MatchPercentage =
                    Math.Round(Math.Clamp(
                        career.MatchPercentage + bonus, 0, 100), 2);

                career.Reason =
                    "Based on your skills, academic performance and interests.";
            }

            return careers
                .OrderByDescending(c => c.MatchPercentage)
                .Take(3)
                .ToList();
        }

        private CareerRecommendation CreateCareer(
            string name,
            CareerAssessment student,
            double score,
            string description,
            string skills)
        {
            return new CareerRecommendation
            {
                CareerName = name,
                MatchPercentage = score,
                Description = description,
                RequiredSkills = skills
            };
        }
    }
}
