import {ApolloClient, InMemoryCache} from "@apollo/client";

const dataSource = localStorage.getItem('dataSource');

export const client = new ApolloClient({
    uri: "https://localhost:7117/graphql",
    cache: new InMemoryCache(),
    defaultOptions: {
        watchQuery: {
            fetchPolicy: "no-cache"
        },
    }
})