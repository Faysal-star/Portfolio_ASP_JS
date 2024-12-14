# Experimental Dynamic & FullStack Portfolio

This project is an Experimental AI-Integrated Dynamic Portfolio. For this project, I wanted to experiment with dynamic adaptability. Whenever I apply for a job, I plan to input the job description into the Admin panel. The frontend will then automatically adjust to match the job description, updating the skill chart and showcasing projects that align with the specific requirements of the job.

<hr>
<u><b>Key Features</b></u>

- SQL Database for  storing Education , Projects , Feedbacks etc.
- Backend & Admin Panel using ASP.NET
- Server using Express.js
- Gemini Pro Model to Summarize Feedbacks
- Sorting best projects according to job, using AI

<hr>

<u><b>Tech Stack</b></u>

- Backend: Node.js, ASP.NET 
- Database: MSSQL 
- Frontend: HTML, CSS, Three.js, GSAP
- AI Integration: Gemini Model for feedback summarization and project sorting

<hr>

## How to Run

1. Import the ``portfolio_DB.sql`` file into MSSMS and generate the Database with some pre-defined data.
2. Update the Connection Strings in ``server>index.js`` and ``admin_panel>Web.config``.
3. Run ``npm install`` inside ``server`` folder to install the dependecies.
4. Get your Gemini API key from [ai.google.dev](https://ai.google.dev) , and paste it into ``server>.env`` as ``API_KEY = _____``
5. Open the ``admin_panel>portfolio_admin.sln`` in Visual Studio and run to view admin panel.
6. Run the ``server>index.js`` . You can use nodemon or ``node index.js``.
7. Run the ``client>index.html`` to view the portfolio. You have to use Liver Server to avoid CORS Policy error.

## Portfolio

![Portfolio](image/README/1710358606556.png "Portfolio")

## Admin Panel

![Edu](image/README/1710358750966.png "Education")

![Projects](image/README/1710358762290.png "Projects")

![Feedback](image/README/1710358770630.png "Feedback Summary")
