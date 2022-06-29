import {gql} from "@apollo/client";

export type StorageSourceChangeInputType = {
    source: string
}


export const CHANGE_SOURCE = gql`
        mutation SetSource($source: String!)
        {
          storage {
            setSource(source: $source){
                currentSource
            }
          }
        }
`
