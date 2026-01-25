if(NOT TARGET tensorflowlite_jni_gms_client::tensorflowlite_jni_gms_client)
add_library(tensorflowlite_jni_gms_client::tensorflowlite_jni_gms_client SHARED IMPORTED)
set_target_properties(tensorflowlite_jni_gms_client::tensorflowlite_jni_gms_client PROPERTIES
    IMPORTED_LOCATION "C:/Users/Arktos/.gradle/caches/8.13/transforms/43aadcab253a8a5b403e98f08478063b/transformed/jetified-play-services-tflite-java-16.4.0/prefab/modules/tensorflowlite_jni_gms_client/libs/android.armeabi-v7a/libtensorflowlite_jni_gms_client.so"
    INTERFACE_INCLUDE_DIRECTORIES "C:/Users/Arktos/.gradle/caches/8.13/transforms/43aadcab253a8a5b403e98f08478063b/transformed/jetified-play-services-tflite-java-16.4.0/prefab/modules/tensorflowlite_jni_gms_client/include"
    INTERFACE_LINK_LIBRARIES ""
)
endif()

