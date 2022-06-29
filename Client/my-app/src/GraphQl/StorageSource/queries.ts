import {gql} from "@apollo/client";

export const FETCH_SOURCES_DATA = gql`
    query GetStorageSourcesData{
  storageSources{
    getStorageSourcesData {
      currentSource
      storageSources
    }
  }
}
`;