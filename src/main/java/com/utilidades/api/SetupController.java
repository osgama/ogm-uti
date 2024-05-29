package com.utilidades.api;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.io.BufferedReader;
import java.io.InputStreamReader;

@RestController
public class SetupController {

    @GetMapping("/setup")
    public String setup(@RequestParam int env) {
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
                    output.append(line).append("\n");
                }
            }

            int exitCode = process.waitFor();
            if (exitCode == 0) {
                return "Setup completed successfully.\n" + output;
            } else {
                return "Setup failed with exit code " + exitCode + ".\n" + output;
            }
        } catch (Exception e) {
            return "Error executing setup script: " + e.getMessage();
        }
    }
}
