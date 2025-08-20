# Access Manager

While working on various projects, I often need a ready-to-use authentication and user management system.
Instead of building it from scratch every time, I created this boilerplate with the essential features already implemented:

- ASP.NET backend
- Angular frontend
- PostgreSQL database
- Built-in authentication and user management

The goal is to speed up the start of new applications — so I can focus on the business logic without wasting time on the basics of authentication and account management.

## Running the Backend Development Server

To run the backend and database for development, follow these steps:

1. **Prepare environment variables:**
    - Go to the `docker` folder.
    - Copy `.env-example` to `.env`:
      ```bash
      cp docker/.env-example docker/.env
      ```
    - Open `docker/.env` and fill in the required variables.

2. **Start the backend and database using Docker Compose:**
    - In the `docker` folder, run:
      ```bash
      docker-compose up
      ```
    - This will start both the ASP.NET backend and the PostgreSQL database using the configuration from `docker-compose.yml`.

Make sure Docker is installed and running on your system before starting these steps.
