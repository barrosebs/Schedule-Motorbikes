# Use a base image for PostgreSQL
FROM postgres:latest

# Set the password for the "postgres" user
ENV POSTGRES_PASSWORD=SMtest

# Set the database environment variable for .NET to connect
ENV POSTGRES_DB=DBScheduleMotorbikes

# Expose the default PostgreSQL port
EXPOSE 5432
