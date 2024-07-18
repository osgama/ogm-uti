package com.utilidades.servicio;

import org.springframework.stereotype.Service;
import com.utilidades.model.*;
import javax.sql.DataSource;
import java.sql.*;
import java.util.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Lazy;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

@Service
public class DatabaseValidationService {

    private static final Logger logger = LoggerFactory.getLogger(DatabaseValidationService.class);

    private final DataSource dataSource;
    private final List<String> users;
    private final List<String> nicknames;

    @Autowired
    public DatabaseValidationService(@Lazy DataSource dataSource, List<String> users, List<String> nicknames) {
        this.dataSource = dataSource;
        this.users = users;
        this.nicknames = nicknames;
        logger.info("Users: {}", users);
        logger.info("Nicknames: {}", nicknames);
    }

    public ConnectionResponse validateConnection(int option) {
        logger.info("Validando conexión para la opción: {}", option);
        logger.info("Tamaño de la lista de usuarios: {}", users.size());
        logger.info("Tamaño de la lista de apodos: {}", nicknames.size());

        if (option < 1 || option > users.size() || option > nicknames.size()) {
            logger.error("Opción inválida: {}", option);
            return new ConnectionResponse("error", "Opción inválida");
        }

        String username = users.get(option - 1);
        String nickname = nicknames.get(option - 1);
        String password = "";

        try {
            // Obtener la contraseña utilizando el servicio de CyberArk
            // SecretAgentClient client = new SecretAgentClient();
            // password = client.getPassword(nickname);
            logger.info("Password retrieved for nickname: {}", nickname);
        } catch (Exception e) {
            logger.error("Error al obtener password para nickname: {}", nickname, e);
            return new ConnectionResponse("error", "Error al obtener password: " + e.getMessage());
        }

        try (Connection connection = dataSource.getConnection(username, password)) {
            if (connection.isValid(2)) {
                logger.info("Conexión exitosa para el usuario: {}", username);
                return new ConnectionResponse("success", "Conexión exitosa");
            } else {
                logger.error("Conexión no exitosa para el usuario: {}", username);
                return new ConnectionResponse("error", "Conexión no exitosa");
            }
        } catch (SQLException e) {
            logger.error("Conexión fallida para el usuario: {}", username, e);
            return new ConnectionResponse("error", "Conexión fallida: " + e.getMessage());
        }
    }
}

