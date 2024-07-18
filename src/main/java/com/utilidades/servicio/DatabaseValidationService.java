package com.utilidades.servicio;


import org.springframework.stereotype.Service;
import com.utilidades.model.*;
import javax.sql.DataSource;
import java.sql.*;
import java.util.*;

@Service
public class DatabaseValidationService {

    private final DataSource dataSource;
    private final List<String> users;
    private final List<String> nicknames;

    public DatabaseValidationService(DataSource dataSource, List<String> users, List<String> nicknames) {
        this.dataSource = dataSource;
        this.users = users;
        this.nicknames = nicknames;
    }

    public ConnectionResponse validateConnection(int option) {
        if (option < 1 || option > users.size() || option > nicknames.size()) {
            return new ConnectionResponse("error", "Opcion invalida");
        }

        String username = users.get(option - 1);
        String nickname = nicknames.get(option - 1);
        String password = "";

        try {
            // Obtener la contraseña utilizando el servicio de CyberArk
            //SecretAgentClient client = new SecretAgentClient();
            //password = client.getPassword(nickname);
        } catch (Exception e) {
            return new ConnectionResponse("error", "Error al obtener password: " + e.getMessage());
        }

        try (Connection connection = dataSource.getConnection(username, password)) {
            if (connection.isValid(2)) {
                return new ConnectionResponse("success", "Conexion success");
            } else {
                return new ConnectionResponse("error", "Conexion no success");
            }
        } catch (SQLException e) {
            return new ConnectionResponse("error", "Conexion fallida: " + e.getMessage());
        }
    }
}