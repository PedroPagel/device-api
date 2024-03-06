# Device API Endpoints

This document provides an overview of the HTTP methods available in the Device API.

## Get All Devices

### Method

- **HTTP Method:** GET
- **Endpoint:** `/devices/get-all`
- **Description:** Retrieves all devices.
- **Returns:** A collection of devices.

## Get Device By Id

### Method

- **HTTP Method:** GET
- **Endpoint:** `/devices/get-by-id/{id}`
- **Description:** Retrieves a device by its unique identifier.
- **Parameters:**
  - `id` (GUID): The unique identifier of the device.
- **Returns:** The device corresponding to the specified ID.

## Get Device By Brand

### Method

- **HTTP Method:** GET
- **Endpoint:** `/devices/get-by-brand/{brand}`
- **Description:** Retrieves a device by its brand.
- **Parameters:**
  - `brand` (String): The brand name of the device.
- **Returns:** The device corresponding to the specified brand.

## Delete Device By Id

### Method

- **HTTP Method:** DELETE
- **Endpoint:** `/devices/delete-by-id/{id}`
- **Description:** Deletes a device by its unique identifier.
- **Parameters:**
  - `id` (GUID): The unique identifier of the device to be deleted.
- **Returns:** A boolean indicating whether the device was successfully deleted.

## Add Device

### Method

- **HTTP Method:** POST
- **Endpoint:** `/devices`
- **Description:** Adds a new device.
- **Request Body:** Device model.
- **Returns:** The newly added device.

## Patch Device

### Method

- **HTTP Method:** PATCH
- **Endpoint:** `/devices/{id}`
- **Description:** Partially updates a device.
- **Parameters:**
  - `id` (GUID): The unique identifier of the device to be updated.
- **Request Body:** Device model with fields to be updated.
- **Returns:** The updated device.

## Put Device

### Method

- **HTTP Method:** PUT
- **Endpoint:** `/devices/{id}`
- **Description:** Updates a device.
- **Parameters:**
  - `id` (GUID): The unique identifier of the device to be updated.
- **Request Body:** Device model with updated fields.
- **Returns:** The updated device.

# Testing with In-Memory Database
For testing purposes, the Device API solution uses an in-memory database. This ensures that tests can be run efficiently without relying on an external database. However, please note that the in-memory database is not persistent and data will be lost when the application is stopped or restarted.

The scripts for creating database and tables. Can be found inside the folder solution.

# Docker Support

The Device API is containerized using Docker, allowing it to be easily deployed and run on various platforms, including Linux.

## Running the API with Docker

To run the Device API using Docker on a Linux environment, follow these steps:

1. Ensure that Docker is installed on your system. If not, you can install Docker by following the instructions on the [Docker website](https://docs.docker.com/get-docker/).

2. Clone or download the repository containing the Device API source code.

3. Navigate to the root directory of the project where the Dockerfile is located.

4. Build the Docker image by running the following command:

   ```bash
   docker build -t devicewebapi:dev .

