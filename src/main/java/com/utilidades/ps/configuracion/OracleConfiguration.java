package com.utilidades.ps.configuracion;

import org.springframework.context.annotation.*;
import oracle.jdbc.pool.OracleDataSource;
import javax.sql.DataSource;
import java.sql.SQLException;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;
import org.slf4j.*;

@Configuration
public class OracleConfiguration {

    private Logger logger = LoggerFactory.getLogger(OracleConfiguration.class);

    @Bean
    @Lazy
    public DataSource dataSource() throws SQLException {
        String urldb = System.getenv("URLDB");

        if (urldb == null || urldb.isEmpty()) {
            throw new IllegalArgumentException("Environment variable URLDB is not set");
        }

        OracleDataSource dataSource = new OracleDataSource();
        dataSource.setURL(urldb);
        dataSource.setDriverType("oracle.jdbc.driver.OracleDriver");
        dataSource.setFastConnectionFailoverEnabled(true);
        dataSource.setImplicitCachingEnabled(true);
        return dataSource;
    }

    @Bean
    public List<String> users() {
        return loadListFromEnv("USERS");
    }

    @Bean
    public List<String> nicknames() {
        return loadListFromEnv("SECRET");
    }

    private List<String> loadListFromEnv(String envVar) {
        String envValue = System.getenv(envVar);
        if (envValue != null && !envValue.isEmpty()) {
            List<String> list = Arrays.stream(envValue.split(","))
                                      .map(String::trim)
                                      .collect(Collectors.toList());
            logger.info("{} cargados: {}", envVar, list);
            return list;
        } else {
            String errorMessage = "No se pudo recuperar la lista de la variable de entorno: " + envVar;
            logger.error(errorMessage);
            throw new IllegalArgumentException(errorMessage);
        }
    }
}