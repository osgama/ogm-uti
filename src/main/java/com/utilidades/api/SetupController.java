package com.utilidades.api;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.util.concurrent.CompletableFuture;

@RestController
public class SetupController {

    private static final Logger logger = LoggerFactory.getLogger(SetupController.class);

    @GetMapping("/setup")
    public CompletableFuture<String> setup(@RequestParam int env) {
        return CompletableFuture.supplyAsync(() -> {
            String[] command;
            switch (env) {
                case 1:
                    command = new String[]{"/bin/bash", "/scripts/setup.sh", "1"};
                    break;
                case 2:
                    command = new String[]{"/bin/bash", "/scripts/setup.sh", "2"};
                    break;
                case 3:
                    command = new String[]{"/bin/bash", "/scripts/setup.sh", "3"};
                    break;
                default:
                    logger.error("Invalid parameter. Use 1 (UAT), 2 (Prod), or 3 (Cob).");
                    return "Invalid parameter. Use 1 (UAT), 2 (Prod), or 3 (Cob).";
            }

            try {
                ProcessBuilder pb = new ProcessBuilder(command);
                pb.redirectErrorStream(true);
                Process process = pb.start();

                StringBuilder output = new StringBuilder();
                try (BufferedReader reader = new BufferedReader(new InputStreamReader(process.getInputStream()))) {
                    String line;
                    while ((line = reader.readLine()) != null) {
                        logger.info(line);
                        output.append(line).append("\n");
                    }
                }

                int exitCode = process.waitFor();
                if (exitCode == 0) {
                    logger.info("Setup completed successfully.");
                    return "Setup completed successfully.\n" + output;
                } else {
                    logger.error("Setup failed with exit code " + exitCode);
                    return "Setup failed with exit code " + exitCode + ".\n" + output;
                }
            } catch (Exception e) {
                logger.error("Error executing setup script: ", e);
                StringWriter sw = new StringWriter();
                e.printStackTrace(new PrintWriter(sw));
                return "Error executing setup script: " + sw.toString();
            }
        });
    }
}

