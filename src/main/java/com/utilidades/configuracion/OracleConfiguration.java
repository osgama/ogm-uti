package com.utilidades.configuracion;

import org.springframework.context.annotation.*;
import oracle.jdbc.pool.OracleDataSource;
import javax.sql.DataSource;
import java.sql.SQLException;
import java.util.*;
import org.slf4j.*;

@Configuration
@Profile("oracle")
public class OracleConfiguration {

    private Logger logger = LoggerFactory.getLogger(OracleConfiguration.class);

    @Bean
    public DataSource dataSource() throws SQLException {
        String urldb = System.getenv("URLDB");

        OracleDataSource dataSource = new OracleDataSource();
        dataSource.setURL(urldb);
        dataSource.setDriverType("oracle.jdbc.driver.OracleDriver");
        dataSource.setFastConnectionFailoverEnabled(true);
        dataSource.setImplicitCachingEnabled(true);
        return dataSource;
    }

    @Bean
    public List<String> users() {
        return Arrays.asList(System.getenv("USERS").split(","));
    }

    @Bean
    public List<String> nicknames() {
        return Arrays.asList(System.getenv("SECRET").split(","));
    }
}