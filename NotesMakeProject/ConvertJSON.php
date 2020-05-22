<?php
    while ($line = fgets(STDIN)) $json .= trim($line);
    $arr = json_decode(mb_convert_encoding($json, 'UTF8', 'ASCII,JIS,UTF-8,EUC-JP,SJIS-WIN'), true);
    if ($arr === NULL) {
        echo "Error: No STDIN.\n";
    } else {
        $file_handler = fopen($arr['name'].".csv", "w");
        for ($i=0; $i<count($arr['notes']); $i++) {
            $line[0] = round(60*$arr['notes'][$i]['num']/($arr['BPM']*$arr['notes'][$i]['LPB']), 6)+$arr['offset']/10000;
            $line[1] = $arr['notes'][$i]['block'];
            fputcsv($file_handler, $line);
        }
        fclose($file_handler);
        echo "> ". __DIR__ . "/" . $arr['name'] . ".csv\n";
    }
?>